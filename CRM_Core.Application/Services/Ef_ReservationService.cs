using CRM_Core.Application.Interfaces;
using CRM_Core.Application.ViewModels.CustomViewModel;
using CRM_Core.DataAccessLayer;
using CRM_Core.DataAccessLayer.Repositories;
using CRM_Core.Entities.Reservation;
using CRM_Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CRM_Core.Application.Services
{
    public class Ef_ReservationService : DataAccessLayer.Repositories.RepositoryBase<Reservation>,IReservationService
    {
        public CRM_CoreDB _context;
        private IPotentialService _potentialService;
        private IPeopleService _peopleService;
        private IChequService _chequeService;
        private string commandText;

        public Ef_ReservationService(CRM_CoreDB context, IPeopleService peopleService, IPotentialService potentialService,IChequService chequeService) : base(context)
        {
            _context = context;
            _peopleService = peopleService;
            _potentialService = potentialService;
            _chequeService = chequeService;
        }

        public bool CheckReservationHasAnyHistoryRecord(int reservationId)
        {
            bool hasAnyHistoryRecord = false;
            Reservation reservation = FindByCondition(item => item.Id == reservationId).OrderBy(item=>item.Id).LastOrDefault();

            if (1==1)
            {
                hasAnyHistoryRecord = false;
            }
            return hasAnyHistoryRecord;
        }

        public IEnumerable<Reservation> GetAllReservation()
        {
            return FindAll();
        }

        public Reservation GetLastReservationId()
        {
            return FindAll().LastOrDefault();
        }

        public PeopleReservationHistoryInfo GetPeopleHistoryReservation(int peopleId)
        {
            List<PeopleReservationHistoryInfo> peopleReservationHistoryInfolist = new List<PeopleReservationHistoryInfo>();
            PeopleReservationHistoryInfo peopleReservationHistoryInfo = new PeopleReservationHistoryInfo();
            return peopleReservationHistoryInfo;
        }

        public DataSet GetReservationByADO(string commandText, string[] searchParameter, object[] searchValues, bool isProcedure)
        {
            SqlConnection conn = new SqlConnection("Server=.; initial Catalog=MoshattehDB; integrated security=true;");
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = commandText;
            cmd.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < searchParameter.Length; i++)
            {
                cmd.Parameters.AddWithValue(searchParameter[i], searchValues[i]);
            }
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();

            da.Fill(ds);
            return ds;
        }

        public IEnumerable<Reservation> GetReservationById(int reservationId)
        {
           return FindByCondition(item=>item.Id == reservationId);
        }

        public Reservation GetReservationByNumber(string number)
        {
            return FindByConditionFirstOrDefault(item=>item.SystemCode == number);
        }

        public IEnumerable<Reservation> GetReservationByParam(int customerId, DateTime reservationDate)
        {
            return FindByCondition(item => item.PeopleId == customerId && item.M_ReservationDate == reservationDate && item.IsActive == true);
        }

        public DataSet GetReservationDetailsADO_ByID(string commandText, string[] searchParameter, object[] searchValues, bool isProcedure = true)
        {
            SqlConnection conn = new SqlConnection("Server=.; initial Catalog=MoshattehDB; integrated security=true;");
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = commandText;
            cmd.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < searchParameter.Length; i++)
            {
                cmd.Parameters.AddWithValue(searchParameter[i], searchValues[i]);
            }
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();

            da.Fill(ds);
            return ds;
        }

        public void InsertReservation(Reservation reservation)
        {
            Create(reservation);
        }

        public void UpdateReservation(Reservation reservation)
        {
            Update(reservation);
        }

    }
}
