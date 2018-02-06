require(["esri/Map", 
    "esri/views/MapView",
    "esri/geometry/geometryEngineAsync",
    "esri/geometry/Polyline",
    "esri/geometry/Polygon",
    "esri/Graphic",
    "esri/layers/FeatureLayer",
    "dojo/domReady!"], 
    function(Map, MapView, geometryEngineAsync, Polyline, Polygon, Graphic, FeatureLayer) {
        var centroidArray = new Array();
        var polygonArray = new Array();
        
        var listofCentroids = [];
        var listofPolygons = [];
        var listofPolygonsSecondHalf = [];
        var listofPolygonsThirdHalf = [];
        var listofPolygonsFourthHalf = [];
        var listofPolygonsFifthHalf = [];
        var listofPolygonsSixthHalf = [];
        var listofPolygonsSeventhHalf = [];
        var listofPolygonsEighthHalf = [];
        var listofPolygonsNinthHalf = [];


        var f1 = new FeatureLayer({
            //This is the url for the Alberta boundary
            url: "https://maps.alberta.ca/genesis/rest/services/GOA_Administrative_Area/Latest/MapServer"
            //This is the url for the Alberta 50k grid
            //url: "https://services2.arcgis.com/jQV6VMr2Loovu7GU/arcgis/rest/services/Alberta_NTS_Grids/FeatureServer/1"
        });

        var map = new Map({
            basemap: "oceans"
        });

        map.add(f1);

        var view = new MapView({
            container: "viewDiv",  
            map: map,          
            center: [-118, 50], 
            zoom: 4    
        });

        var bool = true;
                
        view.whenLayerView(f1).then(function(lyrView){
            lyrView.watch("updating", function(val){
                if(!val && bool){  
                    lyrView.queryFeatures().then(function(results){
                        bool = false;
                        var i = 0;
                        console.log(results[0].geometry);
                        /*
                        setInterval(function(){
                            if(i < results.length){
                                console.log(results[i]);
                            }
                            i++;
                        }, 100);
                        */
                        
                        /*
                        for(let i = 0; i < results.length; i++) {
                            var R = Math.floor(255 * Math.random());
                            var G = Math.floor(255 * Math.random());
                            var B = Math.floor(255 * Math.random());

                            var polygon = new Polygon({
                                hasZ: false,
                                hasM: false,
                                rings: results[i].geometry.rings,
                                spatialReference: 102100
                            });

                            var polygonSymbol = {
                                type: "simple-fill",  // autocasts as new SimpleFillSymbol()
                                color: [R, G, B],
                                style: "solid"
                            };

                            var polygonGraphic = new Graphic({
                                geometry: polygon,
                                symbol: polygonSymbol
                            });
                            view.graphics.add(polygonGraphic);
                        }
                        */
                        var paths = gridCutting(results[0].geometry.extent);
                        var line = new Polyline({
                            hasZ: false,
                            hasM: false,
                            paths: paths,
                            spatialReference: 102100
                          });

                        var polygon = new Polygon({
                            hasZ: false,
                            hasM: false,
                            rings: results[0].geometry.rings,
                            spatialReference: 102100
                        });

                                               
                        /*
                        var polygonGraphic = new Graphic({
                            geometry: polygon,
                            symbol: polygonSymbol
                        });
                        var lineGraphic = new Graphic({
                            geometry: line,
                            symbol: lineSymbol
                        });
                        */

                        geometryEngineAsync.cut(polygon, line).then(function(results){
                            console.log("grid cutting complete");
                            for(let i = 0, l = results.length; i < l; i++) {
                                /*
                                var R = Math.floor(255 * Math.random());
                                var G = Math.floor(255 * Math.random());
                                var B = Math.floor(255 * Math.random());
                                */
                                /*
                                if(results[i].rings[0].length > 5){
                                    continue;
                                }
                                */
                                var polygon = new Polygon({
                                    hasZ: false,
                                    hasM: false,
                                    rings: results[i].rings,
                                    spatialReference: 102100
                                });
                                
                                var polygonSymbol = {
                                    type: "simple-fill",  // autocasts as new SimpleFillSymbol()
                                    color: [255, 255, 255],
                                    style: "solid"
                                }; 
                                                                
                                var polygonGraphic = new Graphic({
                                    geometry: polygon,
                                    symbol: polygonSymbol
                                });
                                
                                centroidArray.push(results[i].centroid);
                                polygonArray.push(results[i].rings);
                                view.graphics.add(polygonGraphic);   
                            }
                            

                            console.log(polygonArray.length);
                            var track = true;
                            polygonArray.map(function(entry, index){
                                var singlePolygonObject = {};
                                singlePolygonObject.rings = entry;
                                listofPolygons.push(singlePolygonObject);
                                /*
                                if(index < polygonArray.length / 9){
                                    listofPolygons.push(singlePolygonObject);
                                }else if(index < polygonArray.length / 9 * 2) {
                                    listofPolygonsSecondHalf.push(singlePolygonObject);
                                }else if(index < polygonArray.length / 9 * 3) {
                                    listofPolygonsThirdHalf.push(singlePolygonObject);
                                }else if(index < polygonArray.length / 9 * 4) {
                                    listofPolygonsFourthHalf.push(singlePolygonObject);
                                }else if(index < polygonArray.length / 9 * 5) {
                                    listofPolygonsFifthHalf.push(singlePolygonObject);
                                }else if(index < polygonArray.length / 9 * 6) {
                                    listofPolygonsSixthHalf.push(singlePolygonObject);
                                }else if(index < polygonArray.length / 9 * 7) {
                                    listofPolygonsSeventhHalf.push(singlePolygonObject);
                                }else if(index < polygonArray.length / 9 * 8) {
                                    listofPolygonsEighthHalf.push(singlePolygonObject);
                                }else {
                                    listofPolygonsNinthHalf.push(singlePolygonObject);
                                }
                                */
                            });

                            centroidArray.map(function(entry, index){
                                var singleCentroidObject = {};
                                singleCentroidObject.lat = entry.latitude;
                                singleCentroidObject.long = entry.longitude;
                                listofCentroids.push(singleCentroidObject);
                            });
                            
                            
                            /*
                            saveText(JSON.stringify(listofPolygons), "listofPolygons.json");
                            saveText(JSON.stringify(listofPolygonsSecondHalf), "listofPolygonsSecondHalf.json");
                            saveText(JSON.stringify(listofPolygonsThirdHalf), "listofPolygonsThirdHalf.json");
                            saveText(JSON.stringify(listofPolygonsFourthHalf), "listofPolygonsFourthHalf.json");
                            saveText(JSON.stringify(listofPolygonsFifthHalf), "listofPolygonsFifthHalf.json");
                            saveText(JSON.stringify(listofPolygonsSixthHalf), "listofPolygonsSixthHalf.json");
                            saveText(JSON.stringify(listofPolygonsSeventhHalf), "listofPolygonsSeventhHalf.json");
                            saveText(JSON.stringify(listofPolygonsEighthHalf), "listofPolygonsEighthHalf.json");
                            saveText(JSON.stringify(listofPolygonsNinthHalf), "listofPolygonsNinthHalf.json");
                            */

                            
                            console.log(listofCentroids);
                            saveText(JSON.stringify(listofCentroids), "listofCentroids.json");
                            
                            
                            console.log(listofPolygons);
                            saveText(JSON.stringify(listofPolygons), "listofPolygons.json");

                            //Code to run in cmd to import JSON file into database
                            //mongo import --jsonArray  --db preliminaryTestingJson --collection selfcreatedJSON \Users\lukez\Desktop\centroidSelfCreated.json
                        });
                    })
                }
            });
        }).catch(function(err){
            console.log(err);
        });   

        
        function gridCutting(geoExtent){
            var numHorCut = 150;
            var numVertCut = 75;
            var paths = new Array();
            var horCutInterval = (geoExtent.ymax - geoExtent.ymin) / numHorCut;
            var vertCutInterval = (geoExtent.xmax - geoExtent.xmin) / numVertCut;
            for(let i = geoExtent.xmin + vertCutInterval; i < geoExtent.xmax - 1; i = i + vertCutInterval) {
                paths.push([[i, geoExtent.ymin],[i, geoExtent.ymax]])
            }
            for(let j = geoExtent.ymin + horCutInterval; j < geoExtent.ymax - 1; j = j + horCutInterval) {
                paths.push([[geoExtent.xmin, j],[geoExtent.xmax, j]]);
            }
            return paths;
        }

        function saveText(text, filename) {
            //https://stackoverflow.com/questions/28464449/how-to-save-json-data-locally-on-the-machine
            var save = document.createElement('a');
            save.setAttribute('href', 'data:text/plain;charset=utf-u,'+encodeURIComponent(text));
            save.setAttribute('download', filename);
            save.click();
        }
})


