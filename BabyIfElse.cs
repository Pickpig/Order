
        

        static int[,] Order(int[,] user ,int[] order)
        {
            //声明1一个二维空数组存储结果；
            int [,] final=new int[5,7];
            bool[] switchOfLine=new bool[5]{false,false,false,false,false};//标记final数组行是否被占用；默认false未被占用。
            //声明一个空数组存放顺序位置数量；
            int[] orderNum=new int[5];
            int done = 100;//赋值后的用户数据做标记；更改元素为100；
            int n = 0;//计数用的；0--->4
            int m = 0;//User专用计数；0--->4
            int x = 0;//为空行数组服务；0--->4
            int k = 0;//循环赋值专用计数；0--->6
            int[] nullArr=new int[5];//用户数据相应位置没有的值的位置
            //确定位置数量数组；
            while (n<5)
            {
                if (user[n,0]==order[0])
                {
                    orderNum[0] = orderNum[0] + 1;//第1个位置的数据量
                }
                if (user[n, 0] == order[1])
                {
                    orderNum[1] = orderNum[1] + 1;//第2个位置的数据量
                } 
                if (user[n, 0] == order[2])
                {
                    orderNum[2] = orderNum[2] + 1;//第3个位置的数据量
                } 
                if (user[n, 0] == order[3])
                {
                    orderNum[3] = orderNum[3] + 1;//第4个位置的数据量
                }
                if (user[n, 0] == order[4])
                {
                    orderNum[4] = orderNum[4] + 1;//第5个位置的数据量
                }
                n = n + 1;
            }
            n = 0;

            while (n<5)//获取空行位置数组；
            {
                if (orderNum[n] == 0)
                {
                    nullArr[n] = n;
                }
                n = n+ 1;
            }
            n = 0;//清零
            //第一次赋值；
            while (n<5)//n final、orderNum服务；
            {
                //if (orderNum[n]==0)
                //{
                //    nullArr[n] = n;
                //}
                 if (orderNum[n]==1)//第一次赋值；n为有数据的位置；只有一个数据的情况；
                {
                    while (m<5)//m为user服务；找路由参数不为0的优先赋值；
                    {
                        if (user[m, 0] == order[n])//此位置的数据值；
                        {
                            if (user[m,1]!=0&&!switchOfLine[n])//第一种情况，路由参数不为0；
                            {
                                //赋值；
                                while (k<7)
                                {
                                    final[n, k] = user[m, k];
                                    k = k + 1;
                                }

                                //赋值后的用户数据做标记；更改元素为100；
                                user[m, 0] = done;
                                //赋值后位置数量为0；
                                //orderNum[n]=0;
                                //位置n被占用；
                                switchOfLine[n] = true;
                                //赋值后k为0；
                                k = 0;
                                break;
                            }
                            else if (user[m, 1] == 0 && !switchOfLine[n])//路由参数为0，其余元素均为0；
                            {
                                //赋值；
                                final[n, 0] = user[m, 0];
                                k = 1;
                                while (k < 7)
                                {
                                  
                                    final[n, k] = 0;
                                    k = k + 1;
                                }
                                //赋值后的用户数据做标记；更改元素为100；
                                user[m, 0] = done;
                                //赋值后位置数量为0；
                               // orderNum[n] = 0;
                                //位置n被占用；
                                switchOfLine[n] = true;
                                //赋值后k为0；
                                k = 0;
                                break;
                            }
                           
                        }
                        m++;
                    }
                    m = 0;//计数清0；
                }
                else if (orderNum[n] > 1)//重复的首字母一样的元素大于2
                {

                    while (m < 5)//m为user服务；遍历user找路由参数不为0的先赋值；
                    {
                        if (user[m, 0] == order[n])//此位置的数据值；
                        {
                            if (user[m, 1] != 0&&!switchOfLine[n])//第一种情况，路由参数不为0；
                            {
                                //赋值；
                                while (k < 7)
                                {
                                    final[n, k] = user[m, k];
                                    k = k + 1;
                                }

                                //赋值后的用户数据做标记；更改元素为100；
                                user[m, 0] = done;
                                //赋值后位置数量减1；
                               // orderNum[n] = orderNum[n]-1;
                                //位置n被占用；
                                switchOfLine[n] = true;
                                //赋值后k为0；
                                k = 0;
                                break;
                            }
                         
                        }
                        m++;
                    }
                    m = 0;//计数清0；
                    while (m < 5)//m为user服务；找路由参数为0的赋值；
                    {
                        if (user[m, 0] == order[n]) //此位置的数据值；
                        {
                            if (user[m, 1] == 0&&!switchOfLine[n]) //第二种情况，路由参数为0；并且位置n未被占用；
                            { 
                                //赋值；
                                final[n, 0] = user[m, 0];
                                k = 1;
                                while (k < 7)
                                {

                                    final[n, k] = 0;
                                    k = k + 1;
                                }
                                //赋值后的用户数据做标记；更改元素为100；
                                user[m, 0] = done;
                                //赋值后位置数量减1；
                               // orderNum[n] = orderNum[n] - 1;
                                //位置n被占用；
                                switchOfLine[n] = true;
                                //赋值后k为0；
                                k = 0;
                                break;
                            }
                        }
                        m++;
                    }
                    m = 0;//计数清0；

                   

                }

                n++;

            }
            n = 0;//计数清零
            //第二次赋值；
            while (x < 5)//遍历空行位置数组；
            {
                if (nullArr[x] > 0)//nullArr[x]为空行位置；也是final为填充的位置；
                {
                    while (m < 5)//遍历用户数组；
                    {
                        if (user[m, 0] != done)//user[m,0]为重复数据的首元素；
                        {
                            if (user[m, 1] != 0 && !switchOfLine[nullArr[x]])//路由参数不为0的情况；赋值
                            {
                                while (k < 7)
                                {
                                    final[nullArr[x], k] = user[m, k];
                                    k = k + 1;
                                }
                                //赋值后的用户数据做标记；更改元素为100；
                                user[m, 0] = done;

                                //空行位置被占用；
                                switchOfLine[nullArr[x]] = true;
                                //赋值后k为0；
                                k = 0;
                                //nullArr[x]行已经赋值；
                                break;

                            }
                            else if (user[m, 1] == 0 && !switchOfLine[nullArr[x]]) //路由参数不为0的情况；赋值
                            {
                                //赋值；
                                final[nullArr[x], 0] = user[m, 0];
                                k = 1;
                                while (k < 7)
                                {

                                    final[nullArr[x], k] = 0;
                                    k = k + 1;
                                }
                               
                                //赋值后的用户数据做标记；更改元素为100；
                                user[m, 0] = done;

                                //空行位置被占用；
                                switchOfLine[nullArr[x]] = true;
                                //赋值后k为0；
                                k = 0;
                                break;
                                
                            }
                        }
                        m++;
                    }
                }
                x++;
            }
            x = 0;//空行计数清0；

            return final;

        }
    


