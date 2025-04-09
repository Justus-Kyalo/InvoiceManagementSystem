using AutoMapper;
using InvoiceManagementSystemAPI.Models;
using InvoiceManagementSystemAPI.Models.Dto;

namespace InvoiceManagementSystemAPI;

public class MappingConfig:Profile
{
    public MappingConfig()
    {
        CreateMap<Invoice, InvoiceDto>();
        CreateMap<InvoiceDto, Invoice>();
        CreateMap<Invoice,InvoiceCreateDto>().ReverseMap();
        CreateMap<Invoice,InvoiceUpdateDto>().ReverseMap();
        CreateMap<Customer, CustomerDto>();
        CreateMap<CustomerDto, Customer>();
        CreateMap<Customer,CustomerCreateDto>().ReverseMap();
        CreateMap<Customer,CustomerUpdateDto>().ReverseMap();
    }
    
}