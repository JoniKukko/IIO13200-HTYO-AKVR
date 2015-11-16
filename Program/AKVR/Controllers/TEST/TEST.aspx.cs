using System;
using System.Collections.Generic;
using AKVR.Services;
using AKVR.Services.Train;
using AKVR.Services.TrafficLocation;

public partial class TEST : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

        var trafficLocationService  = ServiceFactory.TrafficLocation();
        var trafficLocation = trafficLocationService.SelectByStationName("Tampere asema");


        var trainService = ServiceFactory.Train();
        var trains = trainService.SelectByStationShortCode(trafficLocation.stationShortCode);

        result.Text = trains.Count.ToString();
        

    }


}