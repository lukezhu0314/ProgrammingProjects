using System;
using CodeRefactor_Test.Models.ProbabilityModel;
using CodeRefactor_Test.Models.TimeModel;

namespace CodeRefactor_Test.Services
{
    public class NewProbabilityService
    {
        public enum Method { UND = 1, DS, MS, DNE, NULL }

        public enum TrafficConditions { BEST_GUESS = 1, PESSIMISTIC, OPTIMISTIC }

        public enum DepartureTime { SIX_AM = 21600, NINE_AM = 32400, TWELVE_PM = 43200, THREE_PM = 54000, SIX_PM = 64800 }

        public bool UND = false;

        /**********************************************************/
        /*                     Constants                          */
        /**********************************************************/

        /** Mothership Door-to-Groin-Puncture (DTGP) in minutes */
        private double MS_DOOR_TO_GROIN_PUNCTURE = 60;

        /** Mothership Door-to-Needle time (DTN) in minutes */
        private double MS_DOOR_TO_NEEDLE = 30;

        /** Drip n' Ship DTRP in minutes */
        // TODO equal to Mothership DTRP?
        private double DNS_DOOR_TO_GROIN_PUNCTURE = 30;

        /** Drip n' Ship DTN in minutes  */
        private double DNS_DOOR_TO_NEEDLE = 60;

        /** Drip n' Ship Needle-to-Door time (NTD) in minutes */
        private double DNS_NEEDLE_TO_DOOR = 20;

        /** Onset-to-First-Medical-Response time (OFMR) in minutes */
        private double ONSET_FIRST_MEDICAL = 40;

        /** Scene-Time (ST) in minutes */
        private double SCENE_TIME = 20;

        private const string OK_MESSAGE = "OK";

        private const string ZERO_RESULTS_MESSAGE = "ZERO_RESULTS";

        /** Time difference between local region and UTC in hours*/
        private const int TIME_OFFSET = 7;

        /** Minimum Probability of Good Outcome*/
        private const double MIN_PROB = 0.1578;

        /** Set to true if patient has large vessel occlusion */                        //REMOVE
        private const bool LVO = true;

        /** Probability of good outcome for patient suffering a hemorrhagic stroke*/
        private const double HEMORRHAGIC_STROKE_PROB = 0.24;

        /** Probability of good outcome for patient suffering a mimic stroke*/
        private const double MIMIC_STROKE_PROB = 0.9;

        /** Patient Proportions for LAMS screening */
        private ScreeningModel LAMS = new ScreeningModel() { alpha = 0.4538, beta = 0.1092, chi = 0.3445, gamma = 0.0924 };

        /** Patient Proportions for LAMS screening, redistributed */
        private ScreeningModel LAMS2 = new ScreeningModel() { alpha = 0.145, beta = 0.31, chi = 0.104, gamma = 0.441 };

        /** Patient Proportions for RACE screening */
        private ScreeningModel RACE = new ScreeningModel() { alpha = 0.5294, beta = 0.1176, chi = 0.3137, gamma = 0.0392 };

        /** Patient Proportions for C_STAT screening */
        private ScreeningModel C_STAT = new ScreeningModel() { alpha = 0.4, beta = 0.1826, chi = 0.2957, gamma = 0.1217 };

        /** Patient Proportions for LAMS 4-5 triaging*/
        private ScreeningModel LAMS45 = new ScreeningModel() { alpha = 0.4538, beta = 0.1092, chi = 0.3445, gamma = 0.0924 };
        
        /**********************************************************/
        /*                  Instance Variables                    */
        /**********************************************************/

        public ResponseFeature GetProbability(TravelTime travelTime, string screening)
        {
            ResponseFeature response;
            ScreeningModel screen = new ScreeningModel();

            if (screening == "LAMS")
                screen = LAMS;
            else if (screening == "LAMS2")
                screen = LAMS2;
            else if (screening == "RACE")
                screen = RACE;
            else if (screening == "C-STAT")
                screen = C_STAT;
            else if (screening == "LAMS45")
                screen = LAMS45;
            else
                screen = LAMS;

            if (travelTime.DnS_PscTime == null || travelTime.MS_CscTime == null || travelTime.DnS_Psc_CscTime == null)
            {
                response = new ResponseFeature
                {
                    probability = 0,
                    method = Method.DNE.ToString()
                };
                return response;
            }

            // Determines which method is better then return that method and its probability
            double travel_PSC = (double)travelTime.DnS_PscTime / 60;  // travel time from origin to CSC
            double travel_CSC = (double)travelTime.MS_CscTime / 60;  // travel time from origin to PSC
            double travel_PSC_CSC = (double)travelTime.DnS_Psc_CscTime/60;  //travel time from PSC to CSC

            // Mothership's probability of good outcome initially set to minimum probability
            double msProbability; 

            // Mothership's probability of good outcome for LVO patients 
            double msLVOProbability;

            // Mothership's probability of good outcome for SVO patients 
            double msSVOProbability;

            // Drip n' Ship's probability of good outcome initially set to minimum probability
            double dnsProbability;

            // Drip n' Ship's probability of good outcome for LVO patients
            double dnsLVOProbability;

            // Drip n' Ship's probability of good outcome for SVO patients
            double dnsSVOProbability;

            // Determined best method
            Method method = Method.NULL;

            // Determined best method's probability of good outcome
            double probability = -1;

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Probability of good outcome for tPA treatment
            double tPAProbabilityMS;

            // Probability of good outcome for EVT
            double evtProbabilityMS;

            // Probability of good outcome for tPA treatment
            double tPAProbabilityDS;

            // Probability of good outcome for EVT
            double evtProbabilityDS;

            // MOTHERSHIP GOOD OUTCOME PROBABILITY CALCULATIONS
            double t1MS = ONSET_FIRST_MEDICAL + SCENE_TIME + travel_CSC + MS_DOOR_TO_NEEDLE; // Time to drug treatment
            double t2MS = ONSET_FIRST_MEDICAL + SCENE_TIME + travel_CSC + MS_DOOR_TO_GROIN_PUNCTURE;  // Time to groin puncture

             if (t1MS < 4.5 * 60)
            {
                tPAProbabilityMS = ProbTPA_LVO(t1MS);
            }
            else
            {
                tPAProbabilityMS = 0.1328;
            }

            if (ProbEVT_LVO(t2MS) > 0.129)
            {
                evtProbabilityMS = ProbEVT_LVO(t2MS);
            }
            else
            {
                evtProbabilityMS = 0.129;
            }

            msLVOProbability = tPAProbabilityMS + (1 - tPAProbabilityMS) * evtProbabilityMS;


            // Mothership Good Outcome for SVO PAtients
            if (t1MS < 4.5 * 60)
            {
                msSVOProbability = ProbTPA_SVO(t1MS);
            }
            else
            {
                msSVOProbability = 0.4622;
            }

            // Mothership Overall Good Outcome for Patients

            msProbability = (screen.alpha * msLVOProbability) + (screen.beta * msSVOProbability) + (screen.chi * HEMORRHAGIC_STROKE_PROB) + (screen.gamma * MIMIC_STROKE_PROB);

            // DRIP N' SHIP GOOD OUTCOME PROBABILITY CALCULATIONS
            double t1DS = ONSET_FIRST_MEDICAL + SCENE_TIME + travel_PSC + DNS_DOOR_TO_NEEDLE;   // Time to drug treatment
            double t2DS = t1DS + DNS_NEEDLE_TO_DOOR + travel_PSC_CSC + DNS_DOOR_TO_GROIN_PUNCTURE; //Time to groin puncture

            // Drip n' Ship Good Outcome for LVO PAtients
            if (t1DS < 4.5 * 60)
            {
                tPAProbabilityDS = ProbTPA_LVO(t1DS);
            }
            else
            {
                tPAProbabilityDS = 0.1328;
            }

            if (ProbEVT_LVO(t2DS) > 0.129)
            {
                evtProbabilityDS = ProbEVT_LVO(t2DS);
            }
            else
            {
                evtProbabilityDS = 0.129;
            }

            dnsLVOProbability = tPAProbabilityDS + (1 - tPAProbabilityDS) * evtProbabilityDS;

            // Drip n' Ship Good Outcome for SVO PAtients
            if (t1DS < 4.5 * 60)
            {
                dnsSVOProbability = ProbTPA_SVO(t1DS);
            }
            else
            {
                dnsSVOProbability = 0.4622;
            }

            // Drip n' Ship Overall Good Outcome for Patients

            dnsProbability = (screen.alpha * dnsLVOProbability) + (screen.beta * dnsSVOProbability) + (screen.chi * HEMORRHAGIC_STROKE_PROB) + (screen.gamma * MIMIC_STROKE_PROB);

            // Difference between good outcome probabilities of both approaches
            double difference = Math.Abs(msProbability - dnsProbability);

            if (travel_CSC < travel_PSC)
            {
                method = Method.MS;
                probability = msProbability;
                if (difference <= 0.01)
                {
                    UND = true;
                    probability = (dnsProbability + msProbability) / 2;
                }

            }
            else if (dnsProbability > msProbability)
            {
                method = Method.DS;
                probability = dnsProbability;
                if (difference <= 0.01)
                {
                    UND = true;
                    probability = (dnsProbability + msProbability) / 2;
                }
            }
            else if (msProbability > dnsProbability)
            {
                method = Method.MS;
                probability = msProbability;
                if (difference <= 0.01)
                {
                    UND = true;
                    probability = (dnsProbability + msProbability) / 2;
                }
            }


            // Generate response feature
            response = new ResponseFeature
            {
                probability = probability,
                method = method.ToString(),
                status = OK_MESSAGE,
                undefined = UND
            };

            return response;
        }

        public double ProbEVT_LVO(double time)
        {
            // Calculates probability of good outcome for EVT as a function of time (uses new formula) for LVO patients
            return (0.3394 + 0.00000004 * Math.Pow(time, 2) - 0.0002 * time);
        }

        public double ProbTPA_LVO(double time)
        {
            // Calculates probability of good outcome for TPa as a function of time (uses new formula) for LVO patients
            //double numerator = Math.Exp(-0.88496 - 0.00442 * time);
            //return numerator / (1 + numerator);

            // Linear formula
            return (0.2359 + 0.0000002 * Math.Pow(time,2) - 0.0004 * (time));
        }

        public double ProbTPA_SVO(double time)
        {
            // Calculates probability of good outcome for EVT as a function of time (uses new formula) for non-LVO (small vessel occlusion)  patients
            return (0.6343 - 0.00000005 * Math.Pow(time, 2) - 0.0005 * time);
        }
    }
}