using CarWash.DataLayer.DBContext;
using CarWash.DataLayer.SharedEntities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWash.DataLayer.DataManager
{
    public class ServiceDatamanager
    {
        /// <summary>
        /// Master Details
        /// </summary>
        /// <returns>Service master details</returns>
        public List<CarwashMaster> GetMasterDetail()
        {
            List<CarwashMaster> ListMaster = new List<CarwashMaster>();
            using (CarWashConnection con = new CarWashConnection())
            {
                var list = (from records in con.ServiceVacancyMasters select records).ToList();
                var orderlist = (from data in con.ServiceOrders select data).ToList();

                ListMaster = list.Select(g => new CarwashMaster
                {
                    Time = g.Time,
                    id = g.Id,
                    IsVacant = g.Vacancy > orderlist.Where(h => h.ServiceVacId == g.Id).Count()
                }).ToList();
            }

            return ListMaster;
        }

        public List<Project> GetProjects()
        {
            using (CarWashConnection con = new CarWashConnection())
            {
                var res = from data in con.Projects group data by data.ClassNo into data select data.ToList();
                return con.Projects.ToList();
            }
        }

        public List<Project> SaveProject(Project project)
        {
            using (CarWashConnection con = new CarWashConnection())
            {
                con.Projects.Add(project);
                con.SaveChanges();
                return con.Projects.ToList();
            }
        }


        public List<Project> DeleteProject(Project project)
        {
            using (CarWashConnection con = new CarWashConnection())
            {
                var entity = con.Projects.FirstOrDefault(g => g.Id == project.Id);
                if(entity!=null)
                {
                    con.Projects.Remove(entity);
                    con.SaveChanges();
                }
                return con.Projects.ToList();
            }
        }

        /// <summary>
        /// Updates Service Order
        /// </summary>
        /// <param name="Id"></param>
        public async Task UpdateServiceOrder(int Id)
        {
            using (CarWashConnection con = new CarWashConnection())
            {
                ServiceOrder service = new ServiceOrder { ServiceVacId = Id };
                con.ServiceOrders.Add(service);
                await con.SaveChangesAsync();
            }
        }
    }
}