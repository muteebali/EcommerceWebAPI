using AutoMapper;
using OnlinePOSAPI.Models.DTO;
using System.Collections.Generic;

namespace OnlinePOSAPI.Models
{
    public class ModelMapping : Profile
    {
        public ModelMapping()
        {

            CreateMap<Variant, VariantDTO>();
            CreateMap<VariantDTO, Variant>();

            CreateMap<VariantValue, VariantValueDTO>();
            CreateMap<VariantValueDTO, VariantValue>();

            CreateMap<Tag, TagDTO>();
            CreateMap<TagDTO, Tag>();

            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();

            CreateMap<ProductDetailDTO, ProductDetail>();
            CreateMap<ProductDetail, ProductDetailDTO>();

            CreateMap<CategoryDTO, Category>();
            CreateMap<Category, CategoryDTO>();

            CreateMap<OrderDTO, Order>();
            CreateMap<Order, OrderDTO>();

            CreateMap<OrderLineItemDTO, OrderLineItem>();
            CreateMap<OrderLineItem, OrderLineItemDTO>();

            CreateMap<Invoice, InvoiceDTO>();
            CreateMap<InvoiceDTO, Invoice>();


            CreateMap<Promotion, PromotionDTO>();
            CreateMap<PromotionDTO, Promotion>();

            //CreateMap<Order, OrderDetailsDTO>()
            //    .ForMember(dst => dst.OrderLineItems,
            //               opt => opt.MapFrom(src => src.OrderLineItem))
            //    .ForMember(dst => dst.Order,
            //                opt => opt.MapFrom(src=>src.));
        }
    }
}
