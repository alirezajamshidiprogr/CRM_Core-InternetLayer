using CRM_Core.Contract;
using CRM_Core.Contract.People;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Core.Services.People
{
    public class AddPeopleService : IAddPeopleService
    {
        // new 
        public IEnumerable<CRM_Core.DomainLayer.People> GetAllPeople { get; set; }

        private readonly IUnitOfWork unitOfWork;
        public AddPeopleService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public void Execute()
        {
            //unitOfWork.StudentRepository;
            //unitOfWork.CourseRepository;

            unitOfWork.Save();
        }




    }
}
