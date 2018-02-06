using System;
using TC = PacMan.TimeConstant;

namespace PacMan
{
    class ProbabilityCalculation
    {
        public static void Calculation(double x, double z, out double PmRS01_MS, out double PmRS01_DnS) {
            

            double[,] screeningParameters = new double[3, 4] { { 0.45, 0.12, 0.34, 0.09 }, { 0.53, 0.12, 0.31, 0.04 }, { 0.41, 0.18, 0.29, 0.12 } };
            double alpha, beta, gamma, chi;
            alpha = screeningParameters[TC.num - 1, 0];
            beta = screeningParameters[TC.num - 1, 1];
            gamma = screeningParameters[TC.num - 1, 2];
            chi = screeningParameters[TC.num - 1, 3];

            double PmRS01_HS = 0.24;
            double PmRS01_SM = 0.90;

            double t_onset_needle_MS = TC.t_FMC + TC.t_response + TC.t_onscene + z + TC.t_DTN_CSC;
            double t_onset_puncture_MS = TC.t_FMC + TC.t_response + TC.t_onscene + z + TC.t_DTP_MS;
            double t_onset_needle_DnS = TC.t_FMC + TC.t_response + TC.t_onscene + x + TC.t_DTN_PSC;
            double t_onset_puncture_DnS = TC.t_FMC + TC.t_response + TC.t_onscene + x + TC.t_DTN_PSC +TC.t_NTDO + TC.t_PSC_CSC + TC.t_DTP_DnS;

            double PmRS01_alteplaseLVO_MS, PmRS01_EVT_MS, PmRS01_LVO_MS, PmRS01_nLVO_MS;
            double PmRS01_alteplaseLVO_DnS, PmRS01_EVT_DnS, PmRS01_LVO_DnS, PmRS01_nLVO_DnS;

            //Mothership Probabilites
            if (t_onset_needle_MS > 270)
            {
                PmRS01_alteplaseLVO_MS = 0.0968;
            }
            else
            {
                PmRS01_alteplaseLVO_MS = Math.Exp(-0.88496 - 0.00442 * t_onset_needle_MS) / (1 + Math.Exp(-0.88496 - 0.0044 * t_onset_needle_MS));
            }

            if (t_onset_puncture_MS > 1505)
            {
                PmRS01_EVT_MS = 0.129;
            } else
            {
                PmRS01_EVT_MS = 0.3394 + (4E-8) * Math.Pow(t_onset_puncture_MS, 2) - 0.0002 * t_onset_puncture_MS;
            }

            PmRS01_LVO_MS = PmRS01_alteplaseLVO_MS + (1 - PmRS01_alteplaseLVO_MS) * PmRS01_EVT_MS;

            if (t_onset_needle_MS > 270)
            {
                PmRS01_nLVO_MS = 0.4622;
            } else
            {
                PmRS01_nLVO_MS = 0.6343 - (5E-8) * Math.Pow(t_onset_needle_MS, 2) - 0.0005 * t_onset_needle_MS;
            }

            PmRS01_MS = alpha * PmRS01_LVO_MS + beta * PmRS01_nLVO_MS + gamma * PmRS01_HS + chi * PmRS01_SM;

            //DnS Probabilities
            if (t_onset_needle_DnS > 270)
            {
                PmRS01_alteplaseLVO_DnS = 0.0968;
            }
            else
            {
                PmRS01_alteplaseLVO_DnS = Math.Exp(-0.88496 - 0.00442 * t_onset_needle_DnS) / (1 + Math.Exp(-0.88496 - 0.0044 * t_onset_needle_DnS));
            }

            if (t_onset_puncture_DnS > 1505)
            {
                PmRS01_EVT_DnS = 0.129;
            }
            else
            {
                PmRS01_EVT_DnS = 0.3394 + (4E-8) * Math.Pow(t_onset_puncture_DnS, 2) - 0.0002 * t_onset_puncture_DnS;
            }

            PmRS01_LVO_DnS = PmRS01_alteplaseLVO_DnS + (1 - PmRS01_alteplaseLVO_DnS) * PmRS01_EVT_DnS;

            if (t_onset_needle_DnS > 270)
            {
                PmRS01_nLVO_DnS = 0.4622;
            }
            else
            {
                PmRS01_nLVO_DnS = 0.6343 - (5E-8) * Math.Pow(t_onset_needle_DnS, 2) - 0.0005 * t_onset_needle_DnS;
            }

            PmRS01_DnS = alpha * PmRS01_LVO_DnS + beta * PmRS01_nLVO_DnS + gamma * PmRS01_HS + chi * PmRS01_SM;
        }
    }

    class ColorCode
    {
        public static void RGBColor (double P_MS, double P_DnS, out int R, out int G, out int B)
        {
            double frac;
            double Pmax = 0.6;
            double Pmin = 0.4;

            if (P_MS - P_DnS > 0.02)
            {
                frac = (P_MS - Pmin) / (Pmax - Pmin);
                R = Convert.ToInt32(225 * (1 - frac));
                G = 255;
                B = Convert.ToInt32(225 * (1 - frac));
            } else if (P_DnS - P_MS > 0.02)
            {
                frac = (P_DnS - Pmin) / (Pmax - Pmin);
                R = 255;
                G = Convert.ToInt32(225 * (1 - frac));
                B = Convert.ToInt32(225 * (1 - frac));
            } else
            {
                double P = (P_MS + P_DnS) / 2;
                frac = (P - Pmin) / (Pmax - Pmin);
                R = 255;
                G = Convert.ToInt32(165 + 65 * (1 - frac));
                B = Convert.ToInt32(225 * (1 - frac));
            }
        }
    }
}
