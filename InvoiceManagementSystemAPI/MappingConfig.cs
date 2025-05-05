using AutoMapper;
using InvoiceManagementSystemAPI.Models;
using InvoiceManagementSystemAPI.Models.Dto;

namespace InvoiceManagementSystemAPI;

public class MappingConfig:Profile
{
    public MappingConfig()
    {
        CreateMap<Slip, SlipDto>();
        CreateMap<SlipDto, Slip>();
        CreateMap<Slip,SlipCreateDto>().ReverseMap();
        CreateMap<Slip,SlipUpdateDto>().ReverseMap();
        CreateMap<Customer, CustomerDto>();
        CreateMap<CustomerDto, Customer>();
        CreateMap<Customer,CustomerCreateDto>().ReverseMap();
        CreateMap<Customer,CustomerUpdateDto>().ReverseMap();
        CreateMap<Customer, CustomerExtendCIPDto>();
        CreateMap<CustomerExtendCIPDto, Customer>();
        CreateMap<IIFBackup, IIFBackupDto>();
        CreateMap<IIFBackupDto, IIFBackup>();
        CreateMap<Item, ItemDto>();
        CreateMap<ItemDto, Item>();
        CreateMap<Item, ItemCreateDto>().ReverseMap();
        CreateMap<Item, ItemUpdateDto>().ReverseMap();
        CreateMap<Vehicle, VehicleDto>();
        CreateMap<VehicleDto, Vehicle>();
        CreateMap<Vehicle, VehicleCreateDto>().ReverseMap();
        CreateMap<Vehicle, VehicleUpdateDto>().ReverseMap();
        CreateMap<CustomerItemPrice, CustomerItemPriceDto>();
        CreateMap<CustomerItemPriceDto, CustomerItemPrice>();
        CreateMap<CustomerItemPrice, CustomerItemPriceCreateDto>().ReverseMap();
        CreateMap<CustomerItemPrice, CustomerItemPriceUpdateDto>().ReverseMap();
        CreateMap<CustomerItemPrice, CustomerItemPriceTrimDto>();
        CreateMap<CustomerItemPriceTrimDto, CustomerItemPrice>();
    }
    
}