using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;

namespace AKVR.Services.ReasonCategory
{
    public class ReasonCategoryMapper : BaseMapper
    {

        // heitetään factoryltä tulevat clientit basemapperille
        public ReasonCategoryMapper(WebClient webClient, LocalClient localClient, SessionClient sessionClient) : base(webClient, localClient, sessionClient)
        {
        }


        // Model-listana kaikki mitä vr:ltä löytyy
        public List<ReasonCategoryModel> SelectAll()
        {
            // palautusarvo
            List<ReasonCategoryModel> ReasonCategorys;
            
            try
            {
                // get json string
                string json = this.getJSON("metadata/cause-category-codes", true, 3600);
                // Yritetään deserialisoida
                ReasonCategorys = JsonConvert.DeserializeObject<List<ReasonCategoryModel>>(json);
            }
            catch (Exception ex)
            {
                // jos epäonnistuu niin palautetaan ainakin tyhjä lista
                ReasonCategorys = new List<ReasonCategoryModel>();
                Debug.WriteLine("AKVR:SelectAll FAILED: " + ex.Message);
            }
            
            return ReasonCategorys;
        }

    }
}