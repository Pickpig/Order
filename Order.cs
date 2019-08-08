using System.Collections.Generic;
using System;
namespace babyProgram
{
    /// <summary>
    /// 
    /// 参考顺序整型数组：int[] Service_Table；
    /// 客户端的数据二维数组：int[,] Client_Table
    /// 获取最终排序好的数组的方法：对象.FinalArry();即可获得排序的二维数组；
    /// 注意：顺序标识位，不要以0开头； 即Service_Table[]，数组的第一个元素，不能为0；
    /// </summary>
    internal class Order
    {
        public int[] Service_Table; //参考顺序数组；[1,2,3,4,5]
        public int[,] Client_Table; //客户表；二维数组
        public int[,] finalTemp = new int[5, 7]; //最终的排序的二维数组；
        public List<int> nullArea = new List<int>(); //空位置；


        public bool[] OnSwitch = new bool[5] {false, false, false, false, false}; //每行的开关；


        public int[] orderNum = new int[5]; //存放顺序位对应的数据量；
        public List<int[]> packageOfLine = new List<int[]>(); //每行数据包的集合；

        

        public code3(int[,] c, int[] s)
        {
            Client_Table = c;
            Service_Table = s;
        }


        /// <summary>
        ///     元素在用户表内的数量；
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public int InItNum(int a)
        {
            var n = 0; //统计数量
            for (var i = 0; i < Service_Table.Length; i++)
            {
                if (Client_Table[i, 0] == a)
                {
                    n++;
                    // return true;
                }
            }
            return n;
        }

        /// <summary>
        ///     用户数据每行打包成数组的集合；
        /// </summary>
        /// <returns></returns>
        public List<int[]> GetClientDataOfLine()
        {
            var ClientDataOfLine = new List<int[]>(); //Client_Tableb表的行数据集合；


            for (var i = 0; i < 5; i++)
            {
                var dataOfLine = new int[7]; //数据表内每行的数据;每行有7个数据；
                for (var j = 0; j < 7; j++)
                {
                    dataOfLine[j] = Client_Table[i, j];
                }
                ClientDataOfLine.Add(dataOfLine); //每行数据打包成数组，添加到集合中；
            }
            return ClientDataOfLine;
        }

        /// <summary>
        ///     客户表内按照顺序排列的数据数量数组；
        /// </summary>
        /// <returns></returns>
        public int[] GetDataOrderArr()
        {
            var n = 0; //计数用的；
            var temp = new int[5];
            for (var i = 0; i < Service_Table.Length; i++) //固定顺序位的数组；
            {
                for (var j = 0; j < Service_Table.Length; j++)
                {
                    if (Client_Table[j, 0] == Service_Table[i])
                    {
                        n++;
                    }
                }

                temp[i] = n;
                n = 0; //清零计下一个
            }
            return temp;
        }

        /// <summary>
        ///     用户表在顺序表内缺少的位置集合；
        /// </summary>
        /// <returns></returns>
        public List<int> IsGetCTabInSerTaNull()
        {
            var temp = GetDataOrderArr(); //获取顺序数量数组；
            var NullArea = new List<int>(); //服务表内为空的位置的集合；
            for (var i = 0; i < temp.Length; i++)
            {
                if (temp[i] == 0) //空位置
                {
                    NullArea.Add(i);
                }
            }
            return NullArea;
        }
        /// <summary>
        /// 排好不重复的行后，得到的数组；
        /// </summary>
        /// <returns></returns>
        public int[,] GetFirstOrderArr()
        {
            // bool onSwitch = false;//默认没占据行；
            finalTemp = new int[5, 7]; //最终的排序的二维数组；
            packageOfLine = GetClientDataOfLine(); //每行数据包的集合；
            nullArea = IsGetCTabInSerTaNull(); //空位置；

            var temp = GetDataOrderArr(); //获取顺序数量数组；

            for (var i = 0; i < temp.Length; i++) //第一次填充对应位置的数据；
            {
                if (temp[i] != 0) //不是空行的位置；i行
                {
                    //Service_Table[i]放这个数据；


                    for (var k = 0; k < packageOfLine.Count; k++) //遍历包的集合
                    {
                        if (packageOfLine[k][0] == Service_Table[i] && !OnSwitch[i])
                        {
                            if (packageOfLine[k][1] != 0 && CountNotZero(packageOfLine, packageOfLine[k][0]) > 0) //第二位不是0的行存在
                            {
                                for (var j = 0; j < 7; j++) //为不是空行的i行赋值；
                                {
                                    finalTemp[i, j] = packageOfLine[k][j];
                                }
                                OnSwitch[i] = true;
                                packageOfLine.Remove(packageOfLine[k]); //移除排好的行；
                                temp[i]--; //排好后对应位置的个数减1
                            }
                            else if (packageOfLine[k][1] == 0 && !OnSwitch[i] && CountNotZero(packageOfLine, packageOfLine[k][0]) == 0) //
                            {
                                for (var j = 1; j < 7; j++)
                                {
                                    finalTemp[i, 0] = packageOfLine[k][0];
                                    finalTemp[i, j] = 0;
                                }
                                OnSwitch[i] = true;
                                packageOfLine.Remove(packageOfLine[k]); //移除排好的行；
                                temp[i]--; //排好后对应位置的个数减1
                            }
                        }
                    }
                }
            }


            return finalTemp;
        }
        /// <summary>
        /// 最终排好顺序的二维数组；
        /// </summary>
        /// <returns></returns>
        public int[,] FinalArry()
        {
            var n = 0; //用来表示剩下客户数据包；
            var first = GetFirstOrderArr();
            var otherNullLine = new List<int>();
            for (var i = 0; i < 5; i++)
            {
                if (first[i, 0] == 0)
                {
                    otherNullLine.Add(i); //空行的位置添加；
                }
            }

            if (otherNullLine.Count > 0) //如果有空行；赋值
            {
                foreach (var item in otherNullLine)
                {
                    for (var i = 0; i < 7; i++)
                    {
                        if (packageOfLine[0][1] != 0)
                        {
                            first[item, i] = packageOfLine[0][i];
                        }
                        else if (packageOfLine[0][1] == 0)
                        {
                            first[item, 0] = packageOfLine[0][0];
                            //其他位置没赋值，自动变为0；
                        }
                    }
                    packageOfLine.Remove(packageOfLine[0]); //赋值之后删除元素；
                }
            }
            return first;
        }

       /// <summary>
        ///  统计某值开头的行的第二位不是0的行数；
       /// </summary>
       /// <param name="s"></param>
       /// <param name="value">某值</param>
       /// <returns></returns>
        public int CountNotZero(List<int[]> s,int value)
        {
            int n = 0;
           
            for (var t = 0; t < s.Count; t++) //统计第二位不是空行的数量
            {
                if (s[t][0]==value&&s[t][1] != 0)
                {
                    n++;
                }
            }
            return n;
        }
    }
}