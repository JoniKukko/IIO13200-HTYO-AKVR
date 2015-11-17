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
        var trafficLocationList = trafficLocationService.SelectListByStationName("fdsafdsafs");

        result.Text = "";

        foreach (var trafficLocation in trafficLocationList)
        {
            result.Text = result.Text + " " + trafficLocation.stationName;
        }
        

    }


}