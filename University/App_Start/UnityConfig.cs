using BusinessLayer;
using DataAccessLayer;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace University
{
    public static class UnityConfig
    {

        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            return container;
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here  
            //This is the important line to edit  
            
            RegisterTypes(container);
            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IDBConnection, DBConnection>();
            container.RegisterType<IUserDAL, UserDAL>();
            container.RegisterType<IUserBL, UserBL>();
            container.RegisterType<IStudentDAL, StudentDAL>();
            container.RegisterType<IStudentBL, StudentBL>();
        }
    }
}