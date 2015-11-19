using System;
using System.Collections.Generic;
using AKVR.Services;
using AKVR.Services.Train;
using AKVR.Services.TrafficLocation;

public partial class TEST : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

        var service  = ServiceFactory.TrafficLocation();
        var model = service.SelectByStationName(null);


        result.Text = "";
        
            result.Text = result.Text + model.stationName + " (" + model.stationShortCode + ")<br>";
        
        
        

    }


}