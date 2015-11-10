using System;
using AKVR.Services;
using AKVR.Services.Train;
using System.Collections.Generic;

public partial class TEST : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
         

        TrainService trainService = ServiceFactory.Train();
        List<TrainModel> trains = trainService.SelectByStationShortCode("fds");

        result.Text = trains.Count.ToString();

    }


}