using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOLHelpers.Enums
{
    class Enum
    {
    }
    public enum LoggedInnMessage : int
    {
        Success = 1,
        Failure = 2
    }
    public enum UserRole
    {
        Empty = 0,
        Admin = 1,
        Customer = 2
    }

    public enum Invoice
    {
        INV = 1,
        PURINV = 2
    }
    public enum Plans
    {
        Plan = 1
    }

    public enum Permissionsss
    {
        CountryAdd = 1,
        CountryRemove = 2,
        CountryEdit = 3,
        BrandAdd = 4,
        BrandEdit = 5,
        BrandRemove = 6,
        ModelAdd = 7,
        ModelEdit = 8,
        ModelRemove = 9,
        CategoryAdd = 10,
        CategoryEdit = 11,
        CategoryRemove = 12,
        BuyerAdd = 13,
        BuyerEdit = 14,
        BuyerRemove = 15,
        SellerAdd = 16,
        SellerEdit = 17,
        SellerRemove = 18,
        BuyerRequest = 19,
        BuyerRequestedOrders = 20,
        BuyerCompletedOrders = 21,
        SellerRequest = 22,
        SellerOrders = 23,
        EmployeeAdd = 24,
        EmployeeRemove = 25,
        RoleMapping=26,
        RoleAdd=27,
        BuyerPostedRequest=28,
        EmployeeViewAll=29,
        RoleManage=30,
        SellerRating = 31,
        SellerOffers=32

    }
}
