using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace OnlinePOSAPI.Models
{
    public partial class OnlinePOSContext : DbContext
    {
        public OnlinePOSContext()
        {
        }

        public OnlinePOSContext(DbContextOptions<OnlinePOSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Invoice> Invoice { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderLineItem> OrderLineItem { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductDetail> ProductDetail { get; set; }
        public virtual DbSet<ProductMedia> ProductMedia { get; set; }
        public virtual DbSet<ProductMeta> ProductMeta { get; set; }
        public virtual DbSet<ProductTags> ProductTags { get; set; }
        public virtual DbSet<ProductVariant> ProductVariant { get; set; }
        public virtual DbSet<Promotion> Promotion { get; set; }
        public virtual DbSet<ShippingDetail> ShippingDetail { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<Variant> Variant { get; set; }
        public virtual DbSet<VariantValue> VariantValue { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Address)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_address_user");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Deleted).HasColumnName("deleted");

                entity.Property(e => e.ParentCategoryId).HasColumnName("parent_category_id");

                entity.Property(e => e.PromotionId).HasColumnName("promotion_id");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Visible)
                    .IsRequired()
                    .HasColumnName("visible")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Promotion)
                    .WithMany(p => p.Category)
                    .HasForeignKey(d => d.PromotionId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Category_Promotion");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address1)
                    .IsRequired()
                    .HasColumnName("address_1")
                    .HasMaxLength(100);

                entity.Property(e => e.Address2)
                    .IsRequired()
                    .HasColumnName("address_2")
                    .HasMaxLength(100);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasMaxLength(30);

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.Discount)
                    .HasColumnName("discount")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GrandTotal)
                    .HasColumnName("grand_total")
                    .HasComputedColumnSql("((([sub_total]+[shipping_amount])+[tax_amount])-[discount])");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasColumnName("middle_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Mobile)
                    .IsRequired()
                    .HasColumnName("mobile")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modified_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.PaymentMethod)
                    .HasColumnName("payment_method")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasColumnName("postal_code")
                    .HasMaxLength(10);

                entity.Property(e => e.ShippingAmount)
                    .HasColumnName("shipping_amount")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.SubTotal).HasColumnName("sub_total");

                entity.Property(e => e.TaxAmount)
                    .HasColumnName("tax_amount")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_order_user");
            });

            modelBuilder.Entity<OrderLineItem>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modified_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.ProductPricingDetailId).HasColumnName("product_pricing_detail_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Subtotal)
                    .HasColumnName("subtotal")
                    .HasComputedColumnSql("([quantity]*[unit_price])");

                entity.Property(e => e.UnitPrice).HasColumnName("unit_price");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderLineItem)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderLineItem_OrderLineItem");

                entity.HasOne(d => d.ProductPricingDetail)
                    .WithMany(p => p.OrderLineItem)
                    .HasForeignKey(d => d.ProductPricingDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderLineItem_ProductDetail");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Brand)
                    .HasColumnName("brand")
                    .HasMaxLength(30);

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modified_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.PromotionId).HasColumnName("promotion_id");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Visible)
                    .IsRequired()
                    .HasColumnName("visible")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_category_product");

                entity.HasOne(d => d.Promotion)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.PromotionId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Product_Promotion");
            });

            modelBuilder.Entity<ProductDetail>(entity =>
            {
                entity.HasIndex(e => e.ProductId)
                    .HasName("Unique_Product_ID")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BasePrice).HasColumnName("base_price");

                entity.Property(e => e.LongDesc).HasColumnName("long_desc");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.ProductWeight).HasColumnName("product_weight");

                entity.Property(e => e.RetailPrice).HasColumnName("retail_price");

                entity.HasOne(d => d.Product)
                    .WithOne(p => p.ProductDetail)
                    .HasForeignKey<ProductDetail>(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductDetail_Product");
            });

            modelBuilder.Entity<ProductMedia>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Extension)
                    .HasColumnName("extension")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.ProductDetailId).HasColumnName("product_detail_id");

                entity.Property(e => e.Size).HasColumnName("size");

                entity.Property(e => e.Thumbnail).HasColumnName("thumbnail");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnName("URL");

                entity.HasOne(d => d.ProductDetail)
                    .WithMany(p => p.ProductMedia)
                    .HasForeignKey(d => d.ProductDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductDetail_ProductMedia");
            });

            modelBuilder.Entity<ProductMeta>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ImgBlob)
                    .IsRequired()
                    .HasColumnName("img_blob");

                entity.Property(e => e.ProductDetailId).HasColumnName("product_detail_id");

                entity.HasOne(d => d.ProductDetail)
                    .WithMany(p => p.ProductMeta)
                    .HasForeignKey(d => d.ProductDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_productDetail_productMeta");
            });

            modelBuilder.Entity<ProductTags>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.TagId).HasColumnName("tag_id");
            });

            modelBuilder.Entity<ProductVariant>(entity =>
            {
                entity.HasIndex(e => new { e.ProductDetailId, e.VariantValueId })
                    .HasName("Unique_ProductDetailID_VaraintValueID")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Gender)
                    .HasColumnName("gender")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ProductDetailId).HasColumnName("product_detail_id");

                entity.Property(e => e.Sku)
                    .IsRequired()
                    .HasColumnName("sku")
                    .HasMaxLength(30);

                entity.Property(e => e.Stock).HasColumnName("stock");

                entity.Property(e => e.VariantValueId).HasColumnName("variant_value_id");

                entity.HasOne(d => d.ProductDetail)
                    .WithMany(p => p.ProductVariant)
                    .HasForeignKey(d => d.ProductDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductVariant_ProductDetail");

                entity.HasOne(d => d.VariantValue)
                    .WithMany(p => p.ProductVariant)
                    .HasForeignKey(d => d.VariantValueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductVariant_VariantValue");
            });

            modelBuilder.Entity<Promotion>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CouponCode)
                    .HasColumnName("coupon_code")
                    .HasMaxLength(10);

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.DiscountUnit)
                    .IsRequired()
                    .HasColumnName("discount_unit")
                    .HasMaxLength(20);

                entity.Property(e => e.DiscountValue).HasColumnName("discount_value");

                entity.Property(e => e.IsRedeemAllowed)
                    .IsRequired()
                    .HasColumnName("is_redeem_allowed")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MaximumDiscountValue).HasColumnName("maximum_discount_value");

                entity.Property(e => e.MinimumOrderValue).HasColumnName("minimum_order_value");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modified_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(20);

                entity.Property(e => e.ValidFrom)
                    .HasColumnName("valid_from")
                    .HasColumnType("datetime");

                entity.Property(e => e.ValidUntil)
                    .HasColumnName("valid_until")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<ShippingDetail>(entity =>
            {
                entity.ToTable("Shipping_Detail");

                entity.HasIndex(e => e.Id)
                    .HasName("Unique_shipping")
                    .IsUnique();

                entity.HasIndex(e => e.OrderId)
                    .HasName("UQ__Shipping__46596228C26F07E2")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CompanyName)
                    .HasColumnName("company_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContactNumber)
                    .HasColumnName("contact_number")
                    .HasMaxLength(15);

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.ShippedDate)
                    .HasColumnName("shipped_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TrackingId)
                    .HasColumnName("tracking_id")
                    .HasMaxLength(5);
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.LastLogin)
                    .HasColumnName("last_login")
                    .HasColumnType("datetime");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .IsRequired()
                    .HasColumnName("middle_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Mobile)
                    .IsRequired()
                    .HasColumnName("mobile")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modified_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.PasswordHash)
                    .HasColumnName("password_hash")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Role).HasColumnName("role");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Variant>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Attribute)
                    .IsRequired()
                    .HasColumnName("attribute")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modified_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.Visible)
                    .IsRequired()
                    .HasColumnName("visible")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<VariantValue>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.AttributeValue)
                    .IsRequired()
                    .HasColumnName("attribute_value")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modified_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.VariantId).HasColumnName("variant_id");

                entity.Property(e => e.Visible)
                    .IsRequired()
                    .HasColumnName("visible")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Variant)
                    .WithMany(p => p.VariantValue)
                    .HasForeignKey(d => d.VariantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VariantValue_Variant");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
