using AddressCaseStudy.Controller;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace AddressCaseStudy
{
    public partial class Address : System.Web.UI.Page
    {
        AddressController objAddress = new AddressController();
        string[] resultAddress = null;
        public void Page_Load(object sender, EventArgs e)
        {
            resultAddress = objAddress.getAddressFromTXT();
        }

        public void submitaddress_Click(object sender, EventArgs e)
        {
            displayCalculatedFiveAddresses();
        }

        public void displayCalculatedFiveAddresses()
        {
            List<KeyValuePair<string, int>> distance = objAddress.topFiveAddresses(destination.Value.ToLower(), resultAddress);
            Label disLoc = new Label();
            disLoc.Text = "5 closest locations";
            disLoc.Style.Value = "font-weight:bold";
            topFive.Controls.Add(disLoc);
            if (distance != null)
            {
                foreach (var (item, dynamicLabel) in from item in distance
                                                     let dynamicLabel = new Label()
                                                     select (item, dynamicLabel))
                {
                    dynamicLabel.Text = item.Key + "|||| Distance=" + item.Value + "KM";
                    topFive.Controls.Add(dynamicLabel);
                }
            }
        }

    }
}