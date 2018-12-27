using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PremierLeague
{
    class Program
    {

        static void Main(string[] args)
        {
            string teamkey;
            teamkey = Console.ReadLine();
            Console.WriteLine(Run(teamkey));
            Console.ReadKey();
        }
        static public int Run(string teamKey)
        {
            var json_data = string.Empty;
            using (var w = new WebClient())
            {

                json_data = w.DownloadString("https://raw.githubusercontent.com/openfootball/football.json/master/2014-15/en.1.json");

            }
            PremierLeague x = JsonConvert.DeserializeObject<PremierLeague>(json_data);

            int goals = 0;
            bool teamfound;
            foreach (var item in x.rounds)
            {
                teamfound = false;
                int i = 0;
                while (teamfound == false)
                {
                    var match = item.matches[i];
                    if(match.team1.key == teamKey)
                    {
                        goals += Int32.Parse(match.score1);
                        teamfound = true;
                    }
                    else if (match.team2.key == teamKey)
                    {
                        goals += Int32.Parse(match.score2);
                        teamfound = true;
                    }
                    i++;
                }
            }

            return goals;
        }


        public class PremierLeague
        {
            public string name { get; set; }
            public IList<TRounds> rounds { get; set; }
        }
        public class TRounds
        {
            public string name { get; set; }
            public IList<tmatches> matches { get; set; }
        }
        public class tmatches
        {
            public string date { get; set; }
            public team team1 { get; set; }
            public team team2 { get; set; }
            public string score1 { get; set; }
            public string score2 { get; set; }

        }
        public class team
        {
            public string key { get; set; }
            public string name { get; set; }
            public string code { get; set; }
        }
    }
}
