using HairSalonManager.Model.Vo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairSalonManager.Model.Util
{
    class SaveResInColumn //예약을 컬럼에 집어 넣는 클래스
    {

        public static void SaveReservationInColumn(IEnumerable<ReservationVo> necessaryList, DataTable dataTable, DataRow row)
        {
            //각 예약을 집어넣기
            foreach (var item in necessaryList)
            {
                TimeSpan ts = item.EndAt - item.StartAt;
                int result = (ts.Hours * 60) + ts.Minutes;
                int block = result / 30;
                int i = 0;

                foreach (DataColumn column in dataTable.Columns)
                {
                    if (column.ColumnName.Equals(item.StartAt.Hour.ToString("D2") + " : " + item.StartAt.Minute.ToString("D2")))
                    {
                        for (int j = 0; j < block; j++)
                        {
                            row[i] = item.ResNum;
                            i++;
                        }
                    }
                    i++;
                }
            }
            dataTable.Rows.Add(row);
        }
    }
}
