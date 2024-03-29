﻿using MaverickBankk.Models.DTOs;
using MaverickBankk.Models;

namespace MaverickBankk.Interfaces
{
    public interface IAdministratorCustomerManagementService
    {

        Task<Customers?> DeactivateUser(int customerId);
        Task<Customers?> GetUser(int customerId);
        Task<List<Customers>?> GetAllUsers();
        Task<Customers?> UpdateCustomerName(int customerId, AdminUpdateCustomerNameDTO nameDTO);
        Task<Customers?> UpdateCustomerContact(int customerId, AdminUpdateCustomerContactDTO contactDTO);
        Task<Customers?> UpdateCustomerDetails(int customerId, AdminUpdateCustomerDetailsDTO detailsDTO);
        Task<Customers?> ActivateUser(int customerId);
        Task<Customers?> CreateCustomer(RegisterCustomerDTO customerDTO);

    }
}
