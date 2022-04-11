using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CRM_Core.Infrastructure
{
    public static class Enums
    {
      public enum tbasPeopePropertyState
      {
            code = 1 ,
            number = 2,
            commentTel = 3,
            mobile = 4 ,
            mobileComment = 5,
        }

        public enum tbasCategoryState
        {
            personnel = 2 ,
        }

        public enum states
        {
            People = 1 ,
            Reservation = 2 ,
            
        }

        public enum costs
        {
            [Description("هزینه های با پرداخت")]
            CostWithPayment = 1,
            [Description("هزینه های بدون پرداخت")]
            CostWithoutPayment = 2,

            [Description("هزینه های عمومی و اداری")]
            GeneralAndOfficeCosts = 1,
            [Description("هزینه های توزیع و فروش")]
            SellAndDistibuteCost = 2,
            [Description("هزینه های مالی در حسابرسی")]
            AccontantCosts = 3,
            [Description("هزینه استهلاک اموال")]
            Depriciation = 4,
            
            [Description("هزینه قبوض")]
            Bills = 5,
            [Description("رفت و امد")]
            Transfer = 6,
            [Description("تنقلات")]
            Foods = 7,
            [Description("حقوق و دستمزد")]
            Salary = 8,
            [Description("هزینه های عمومی")]
            GeneralCosts = 27,

            [Description("انبار داری")]
            Warehosing = 9,
            [Description("بازاریابی")]
            Marketing = 10,
            [Description("بسته بندی")]
            Packing = 11,
            [Description("تبلیغات")]
            Advertisment = 12,


            [Description("حسابداری")]
            Accounting = 13,
            [Description("حسابرسی")]
            Audit = 14,
            [Description("سود اقساط بانکی")]
            BankBenifit = 15,
            [Description("جرائم مالیاتی")]
            FinancePenalties = 16,
            [Description("مشاوره مالی")]
            FinanceConsulting = 17,

            [Description("قبض گاز")]
            GasBill = 1,
            [Description("قبض برق")]
            PowerBill = 2,
            [Description("قبض آب")]
            WaterBill = 3,
            [Description("قبض تلفن")]
            TelBill = 4,
            [Description("سایر قبوض")]
            OtherBill = 0,

            //[Description("فرسوده شده اسباب و اثاثیه")]
            //DecayBelongings = 1,
            //[Description("هزینه استهلاک ابزار آلات")]
            //Depreciation = 2,

            [Description("سایر ")]
            OtherTransfer = 0,
            [Description("اسنپ")]
            Snapp = 1,
            [Description("تاکسی")]
            Taxi = 2,
            [Description("پیک موتور")]
            MotorCylePost = 3,
            [Description("پست")]
            Post = 4,
        }

        public enum PeopleCategory
        {
            Customer = 1 ,
            Personnel= 2,
        }

        public enum ButtonType
        {
            RegisterBotton = 1,
            CancelBotton = 2,
            SearchBotton = 3,
            clearBotton = 4,
        }

        public enum PeopleType
        {
            people = 1 ,
            contact = 2,
            personnel = 3,
        }

        public enum MariedType
        {
            Single = 1,
            Married = 2,
            divorced = 3,
        }
    }
}
