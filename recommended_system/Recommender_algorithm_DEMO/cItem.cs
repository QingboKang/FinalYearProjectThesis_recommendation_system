using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Recommendation_Algorithm
{
    /// <summary>
    /// 电影信息类
    /// </summary>
    public class movieInfo
    {
        public int  movie_id;       // 电影id
        public string name;         // 电影片名
        public string ReleaseDate;  // 电影上映日期
        public string[] genres;     // 影片类型
    }

    public class cItem
    {
        // 影片类型
        private static string[] sGenres = { "未知", "动作", "冒险", "动画" , "儿童",  
                                           "喜剧", "犯罪", "记录片",  "剧情",
                                           "幻想", "黑色", "恐怖", "音乐",
                                           "神秘", "爱情", "科幻", "惊悚",
                                           "战争",  "西部" };

        public static movieInfo[] movies = new movieInfo[1683];

        public cItem()
        {
            readMovieInfo();
        }

        private static void readMovieInfo()
        {
            StreamReader rs = new StreamReader("u.item", Encoding.Default);
            string sLine = "";
            int count = 1;
            string temp_1, temp_2, temp_3;

            while (sLine != null)
            {
                // 读取一行即一部电影的信息
                sLine = rs.ReadLine();
                if (sLine == null)
                    break;

                // 初始化电影信息对象
                movies[count] = new movieInfo();
                movies[count].movie_id = count;
                movies[count].genres = new string[19];

                // 分解字符串，得到电影片名
                int start = sLine.IndexOf('|') + 1;
                int end = sLine.IndexOf('(');
                if (end > start)
                {
                    string name = sLine.Substring(start, end - start);
                    if (name.EndsWith("The "))
                    {
                        name = "The " + name.Substring(0, name.Length - 6);

                    }
                    movies[count].name = name;
                }
                else
                {
                    movies[count].name = "unknown";
                }

                // 分解字符串，得到电影上映日期
                temp_1 = sLine.Substring(sLine.IndexOf('|') + 1);
                temp_2 = temp_1.Substring(temp_1.IndexOf('|') + 1);
                movies[count].ReleaseDate = temp_2.Substring(0, temp_2.IndexOf('|'));

                temp_3 = temp_2.Substring(temp_2.IndexOf('|') + 2);
                temp_3 = temp_3.Substring(temp_3.IndexOf('|') + 1);

                for (int i = 0; i < sGenres.Length; i++)
                {
                    // 此电影有此类型
                    if (temp_3[i * 2] == '1')
                    {
                        movies[count].genres[i] = sGenres[i]; 
                    }
                    else
                    {
                        movies[count].genres[i] = "";
                    }
                }
                count++;
            }
        }
    }
}
