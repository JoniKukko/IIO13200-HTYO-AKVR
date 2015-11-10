using System;
using AKVR.Services;
using AKVR.Services.Train;

public partial class TEST : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        
        string query = "5423543";
        int intQuery;
        Int32.TryParse(query, out intQuery);    

        TrainService trainService = ServiceFactory.Train();
        TrainModel resultTrain = trainService.SelectByTrainNumber(5423543);

        result.Text = resultTrain.ToString();

    }


}