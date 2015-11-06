using System;
using AKVR.Services;
using AKVR.Services.Train;

public partial class TEST : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        // just a simple test to test this test

        // jos factoryn metodille antaa truen niin saa väliaikaisesti fromWebin päälle
        // Factorystä löytyy myös pysyvä asetus sille, sen pitäis ehkä olla jossain web.configissa oikeasti
        TrainService trainService = ServiceFactory.Train(true);
        TrainModel train = trainService.Select(1);

        try
        {
            result.Text = train.trainCategory + " " + train.trainType + " " + train.trainNumber.ToString();
        } catch (Exception ex)
        {
            result.Text = "Failed to use train-model :( Something went wrong";
        }
        
    }


}