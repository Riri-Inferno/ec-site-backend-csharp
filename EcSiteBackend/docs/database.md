## Diagram

```mermaid
erDiagram

    __EFMigrationsHistory {
        MigrationId character_varying PK "not null"
        ProductVersion character_varying "not null"
    }

    cart_items {
        Id uuid PK "not null"
        CartId uuid FK "not null"
        ProductId uuid FK "not null"
        IsDeleted boolean "not null"
        IsSavedForLater boolean "not null"
        RowVersion bytea "not null"
        Quantity integer "not null"
        PriceAtAdded numeric "not null"
        AddedAt timestamp_with_time_zone "not null"
        CreatedAt timestamp_with_time_zone "not null"
        DeletedAt timestamp_with_time_zone "null"
        UpdatedAt timestamp_with_time_zone "null"
        CreatedBy uuid "null"
        DeletedBy uuid "null"
        UpdatedBy uuid "null"
    }

    carts {
        Id uuid PK "not null"
        UserId uuid FK "not null"
        IsDeleted boolean "not null"
        RowVersion bytea "not null"
        CreatedAt timestamp_with_time_zone "not null"
        LastActivityAt timestamp_with_time_zone "not null"
        SessionId character_varying "null"
        DeletedAt timestamp_with_time_zone "null"
        ExpiresAt timestamp_with_time_zone "null"
        UpdatedAt timestamp_with_time_zone "null"
        CreatedBy uuid "null"
        DeletedBy uuid "null"
        UpdatedBy uuid "null"
    }

    categories {
        Id uuid PK "not null"
        ParentCategoryId uuid FK "null"
        IsActive boolean "not null"
        IsDeleted boolean "not null"
        RowVersion bytea "not null"
        Name character_varying "not null"
        Slug character_varying "not null"
        DisplayOrder integer "not null"
        CreatedAt timestamp_with_time_zone "not null"
        Description character_varying "null"
        ImageUrl character_varying "null"
        DeletedAt timestamp_with_time_zone "null"
        UpdatedAt timestamp_with_time_zone "null"
        CreatedBy uuid "null"
        DeletedBy uuid "null"
        UpdatedBy uuid "null"
    }

    coupons {
        Id uuid PK "not null"
        TargetCategoryId uuid FK "null"
        IsActive boolean "not null"
        IsDeleted boolean "not null"
        RowVersion bytea "not null"
        Code character_varying "not null"
        Name character_varying "not null"
        DiscountType integer "not null"
        UsedCount integer "not null"
        CreatedAt timestamp_with_time_zone "not null"
        ValidFrom timestamp_with_time_zone "not null"
        ValidTo timestamp_with_time_zone "not null"
        Description character_varying "null"
        UsageLimitPerUser integer "null"
        UsageLimitTotal integer "null"
        DiscountAmount numeric "null"
        DiscountRate numeric "null"
        MaxDiscountAmount numeric "null"
        MinimumPurchaseAmount numeric "null"
        DeletedAt timestamp_with_time_zone "null"
        UpdatedAt timestamp_with_time_zone "null"
        CreatedBy uuid "null"
        DeletedBy uuid "null"
        UpdatedBy uuid "null"
    }

    favorites {
        Id uuid PK "not null"
        ProductId uuid FK "not null"
        UserId uuid FK "not null"
        IsDeleted boolean "not null"
        IsNotificationEnabled boolean "not null"
        RowVersion bytea "not null"
        AddedAt timestamp_with_time_zone "not null"
        CreatedAt timestamp_with_time_zone "not null"
        Note character_varying "null"
        DeletedAt timestamp_with_time_zone "null"
        UpdatedAt timestamp_with_time_zone "null"
        CreatedBy uuid "null"
        DeletedBy uuid "null"
        UpdatedBy uuid "null"
    }

    order_histories {
        Id uuid PK "not null"
        OriginalId uuid FK "not null"
        OrderNumber character_varying "not null"
        OperationType integer "not null"
        Status integer "not null"
        DiscountAmount numeric "not null"
        ShippingFee numeric "not null"
        SubTotal numeric "not null"
        TaxAmount numeric "not null"
        TotalAmount numeric "not null"
        BillingAddress text "not null"
        ShippingAddress text "not null"
        OperatedAt timestamp_with_time_zone "not null"
        OrderDate timestamp_with_time_zone "not null"
        OperatedBy uuid "not null"
        UserId uuid "not null"
        AdminNote character_varying "null"
        CustomerNote character_varying "null"
        AfterJson text "null"
        BeforeJson text "null"
        ChangeReason text "null"
        IpAddress text "null"
        UserAgent text "null"
        PaymentMethodId uuid "null"
        ShippingMethodId uuid "null"
    }

    order_items {
        Id uuid PK "not null"
        OrderId uuid FK "not null"
        ProductId uuid FK "not null"
        IsDeleted boolean "not null"
        RowVersion bytea "not null"
        ProductName character_varying "not null"
        ProductSku character_varying "not null"
        Quantity integer "not null"
        DiscountAmount numeric "not null"
        TaxAmount numeric "not null"
        UnitPrice numeric "not null"
        CreatedAt timestamp_with_time_zone "not null"
        DeletedAt timestamp_with_time_zone "null"
        UpdatedAt timestamp_with_time_zone "null"
        CreatedBy uuid "null"
        DeletedBy uuid "null"
        UpdatedBy uuid "null"
    }

    order_status_histories {
        Id uuid PK "not null"
        OrderId uuid FK "not null"
        IsDeleted boolean "not null"
        IsNotificationSent boolean "not null"
        RowVersion bytea "not null"
        ToStatus integer "not null"
        CreatedAt timestamp_with_time_zone "not null"
        Note character_varying "null"
        Reason character_varying "null"
        FromStatus integer "null"
        DeletedAt timestamp_with_time_zone "null"
        NotificationSentAt timestamp_with_time_zone "null"
        UpdatedAt timestamp_with_time_zone "null"
        CreatedBy uuid "null"
        DeletedBy uuid "null"
        UpdatedBy uuid "null"
    }

    orders {
        Id uuid PK "not null"
        UserId uuid FK "not null"
        PaymentMethodId uuid FK "null"
        PaymentMethodId1 uuid FK "null"
        ShippingMethodId uuid FK "null"
        ShippingMethodId1 uuid FK "null"
        IsDeleted boolean "not null"
        RowVersion bytea "not null"
        OrderNumber character_varying "not null"
        Status integer "not null"
        DiscountAmount numeric "not null"
        ShippingFee numeric "not null"
        SubTotal numeric "not null"
        TaxAmount numeric "not null"
        TotalAmount numeric "not null"
        BillingAddress text "not null"
        ShippingAddress text "not null"
        CreatedAt timestamp_with_time_zone "not null"
        OrderDate timestamp_with_time_zone "not null"
        AdminNote character_varying "null"
        CouponCode character_varying "null"
        CustomerNote character_varying "null"
        CancelledAt timestamp_with_time_zone "null"
        DeletedAt timestamp_with_time_zone "null"
        DeliveredAt timestamp_with_time_zone "null"
        PaidAt timestamp_with_time_zone "null"
        PaymentDueDate timestamp_with_time_zone "null"
        ShippedAt timestamp_with_time_zone "null"
        UpdatedAt timestamp_with_time_zone "null"
        CreatedBy uuid "null"
        DeletedBy uuid "null"
        UpdatedBy uuid "null"
    }

    password_reset_tokens {
        Id uuid PK "not null"
        UserId uuid FK "not null"
        IsDeleted boolean "not null"
        IsUsed boolean "not null"
        RowVersion bytea "not null"
        TokenHash character_varying "not null"
        CreatedAt timestamp_with_time_zone "not null"
        ExpiresAt timestamp_with_time_zone "not null"
        RequestIpAddress character_varying "null"
        UsedIpAddress character_varying "null"
        DeletedAt timestamp_with_time_zone "null"
        UpdatedAt timestamp_with_time_zone "null"
        UsedAt timestamp_with_time_zone "null"
        CreatedBy uuid "null"
        DeletedBy uuid "null"
        UpdatedBy uuid "null"
    }

    payment_histories {
        Id uuid PK "not null"
        PaymentId uuid FK "not null"
        IsSuccess boolean "not null"
        OperationType integer "not null"
        ToStatus integer "not null"
        Amount numeric "not null"
        OperatedAt timestamp_with_time_zone "not null"
        OperatedBy uuid "not null"
        OriginalId uuid "not null"
        ErrorMessage character_varying "null"
        TransactionId character_varying "null"
        FromStatus integer "null"
        RefundAmount numeric "null"
        AfterJson text "null"
        BeforeJson text "null"
        ChangeReason text "null"
        IpAddress text "null"
        ResponseData text "null"
        UserAgent text "null"
    }

    payment_methods {
        Id uuid PK "not null"
        IsActive boolean "not null"
        IsDeleted boolean "not null"
        RowVersion bytea "not null"
        Code character_varying "not null"
        Name character_varying "not null"
        DisplayOrder integer "not null"
        FeeAmount numeric "not null"
        FeeRate numeric "not null"
        CreatedAt timestamp_with_time_zone "not null"
        Description character_varying "null"
        IconUrl character_varying "null"
        MaximumAmount numeric "null"
        MinimumAmount numeric "null"
        DeletedAt timestamp_with_time_zone "null"
        UpdatedAt timestamp_with_time_zone "null"
        CreatedBy uuid "null"
        DeletedBy uuid "null"
        UpdatedBy uuid "null"
    }

    payments {
        Id uuid PK "not null"
        OrderId uuid FK "not null"
        PaymentMethodId uuid FK "not null"
        IsDeleted boolean "not null"
        RowVersion bytea "not null"
        Status integer "not null"
        Amount numeric "not null"
        RefundedAmount numeric "not null"
        CreatedAt timestamp_with_time_zone "not null"
        FailureReason character_varying "null"
        Note character_varying "null"
        ProviderName character_varying "null"
        RefundReason character_varying "null"
        TransactionId character_varying "null"
        ResponseData text "null"
        CancelledAt timestamp_with_time_zone "null"
        DeletedAt timestamp_with_time_zone "null"
        DueDate timestamp_with_time_zone "null"
        FailedAt timestamp_with_time_zone "null"
        PaidAt timestamp_with_time_zone "null"
        RefundedAt timestamp_with_time_zone "null"
        UpdatedAt timestamp_with_time_zone "null"
        CreatedBy uuid "null"
        DeletedBy uuid "null"
        UpdatedBy uuid "null"
    }

    product_categories {
        CategoryId uuid PK "not null"
        ProductId uuid PK "not null"
        CategoryId uuid FK "not null"
        ProductId uuid FK "not null"
        IsPrimary boolean "not null"
        CreatedAt timestamp_with_time_zone "not null"
    }

    product_histories {
        Id uuid PK "not null"
        OriginalId uuid FK "not null"
        IsPublished boolean "not null"
        Name character_varying "not null"
        Sku character_varying "not null"
        OperationType integer "not null"
        Status integer "not null"
        Price numeric "not null"
        OperatedAt timestamp_with_time_zone "not null"
        OperatedBy uuid "not null"
        Description character_varying "null"
        Weight integer "null"
        CostPrice numeric "null"
        ListPrice numeric "null"
        AfterJson text "null"
        BeforeJson text "null"
        ChangeReason text "null"
        DetailDescription text "null"
        IpAddress text "null"
        UserAgent text "null"
        PublishedAt timestamp_with_time_zone "null"
    }

    product_images {
        Id uuid PK "not null"
        ProductId uuid FK "not null"
        IsDeleted boolean "not null"
        IsMain boolean "not null"
        RowVersion bytea "not null"
        ImageUrl character_varying "not null"
        DisplayOrder integer "not null"
        CreatedAt timestamp_with_time_zone "not null"
        FileSize bigint "null"
        AltText character_varying "null"
        ThumbnailUrl character_varying "null"
        Title character_varying "null"
        Height integer "null"
        Width integer "null"
        DeletedAt timestamp_with_time_zone "null"
        UpdatedAt timestamp_with_time_zone "null"
        CreatedBy uuid "null"
        DeletedBy uuid "null"
        UpdatedBy uuid "null"
    }

    products {
        Id uuid PK "not null"
        IsDeleted boolean "not null"
        IsPublished boolean "not null"
        RowVersion bytea "not null"
        Name character_varying "not null"
        Sku character_varying "not null"
        Slug character_varying "not null"
        Status integer "not null"
        Price numeric "not null"
        CreatedAt timestamp_with_time_zone "not null"
        Description character_varying "null"
        MetaDescription character_varying "null"
        MetaTitle character_varying "null"
        Weight integer "null"
        CostPrice numeric "null"
        ListPrice numeric "null"
        DetailDescription text "null"
        DeletedAt timestamp_with_time_zone "null"
        PublishedAt timestamp_with_time_zone "null"
        UpdatedAt timestamp_with_time_zone "null"
        CreatedBy uuid "null"
        DeletedBy uuid "null"
        UpdatedBy uuid "null"
    }

    reviews {
        Id uuid PK "not null"
        ProductId uuid FK "not null"
        UserId uuid FK "not null"
        OrderId uuid FK "null"
        IsApproved boolean "not null"
        IsDeleted boolean "not null"
        IsPublished boolean "not null"
        IsVerifiedPurchase boolean "not null"
        RowVersion bytea "not null"
        Comment character_varying "not null"
        HelpfulCount integer "not null"
        Rating integer "not null"
        CreatedAt timestamp_with_time_zone "not null"
        Title character_varying "null"
        ApprovedAt timestamp_with_time_zone "null"
        DeletedAt timestamp_with_time_zone "null"
        UpdatedAt timestamp_with_time_zone "null"
        ApprovedBy uuid "null"
        CreatedBy uuid "null"
        DeletedBy uuid "null"
        UpdatedBy uuid "null"
    }

    roles {
        Id uuid PK "not null"
        IsDeleted boolean "not null"
        IsSystemRole boolean "not null"
        RowVersion bytea "not null"
        Name character_varying "not null"
        DisplayOrder integer "not null"
        CreatedAt timestamp_with_time_zone "not null"
        Description character_varying "null"
        DeletedAt timestamp_with_time_zone "null"
        UpdatedAt timestamp_with_time_zone "null"
        CreatedBy uuid "null"
        DeletedBy uuid "null"
        UpdatedBy uuid "null"
    }

    shipping_methods {
        Id uuid PK "not null"
        IsActive boolean "not null"
        IsDeleted boolean "not null"
        IsTrackable boolean "not null"
        RowVersion bytea "not null"
        Code character_varying "not null"
        Name character_varying "not null"
        DisplayOrder integer "not null"
        EstimatedDays integer "not null"
        BaseFee numeric "not null"
        CreatedAt timestamp_with_time_zone "not null"
        CarrierName character_varying "null"
        Description character_varying "null"
        FreeShippingMinAmount numeric "null"
        DeletedAt timestamp_with_time_zone "null"
        UpdatedAt timestamp_with_time_zone "null"
        CreatedBy uuid "null"
        DeletedBy uuid "null"
        UpdatedBy uuid "null"
    }

    shippings {
        Id uuid PK "not null"
        OrderId uuid FK "not null"
        ShippingMethodId uuid FK "not null"
        IsDeleted boolean "not null"
        RowVersion bytea "not null"
        Status integer "not null"
        ShippingFee numeric "not null"
        ShippingAddress text "not null"
        CreatedAt timestamp_with_time_zone "not null"
        CarrierName character_varying "null"
        Note character_varying "null"
        TrackingNumber character_varying "null"
        ActualDeliveryDate timestamp_with_time_zone "null"
        ActualShipDate timestamp_with_time_zone "null"
        DeletedAt timestamp_with_time_zone "null"
        EstimatedDeliveryDate timestamp_with_time_zone "null"
        EstimatedShipDate timestamp_with_time_zone "null"
        UpdatedAt timestamp_with_time_zone "null"
        CreatedBy uuid "null"
        DeletedBy uuid "null"
        UpdatedBy uuid "null"
    }

    stock_histories {
        Id uuid PK "not null"
        StockId uuid FK "not null"
        RelatedOrderId uuid FK "null"
        MovementType integer "not null"
        OperationType integer "not null"
        QuantityAfter integer "not null"
        QuantityBefore integer "not null"
        OperatedAt timestamp_with_time_zone "not null"
        OperatedBy uuid "not null"
        OriginalId uuid "not null"
        Note character_varying "null"
        AfterJson text "null"
        BeforeJson text "null"
        ChangeReason text "null"
        IpAddress text "null"
        UserAgent text "null"
    }

    stocks {
        Id uuid PK "not null"
        ProductId uuid FK "not null"
        AllowBackorder boolean "not null"
        IsDeleted boolean "not null"
        IsTrackingEnabled boolean "not null"
        RowVersion bytea "not null"
        MinStockLevel integer "not null"
        Quantity integer "not null"
        ReservedQuantity integer "not null"
        SafetyStockLevel integer "not null"
        CreatedAt timestamp_with_time_zone "not null"
        MaxStockLevel integer "null"
        DeletedAt timestamp_with_time_zone "null"
        LastInventoryDate timestamp_with_time_zone "null"
        UpdatedAt timestamp_with_time_zone "null"
        CreatedBy uuid "null"
        DeletedBy uuid "null"
        UpdatedBy uuid "null"
    }

    system_settings {
        Id uuid PK "not null"
        IsCacheable boolean "not null"
        IsDeleted boolean "not null"
        IsEditable boolean "not null"
        IsEncrypted boolean "not null"
        RowVersion bytea "not null"
        Category character_varying "not null"
        DataType character_varying "not null"
        Key character_varying "not null"
        Value character_varying "not null"
        DisplayOrder integer "not null"
        CreatedAt timestamp_with_time_zone "not null"
        Description character_varying "null"
        DeletedAt timestamp_with_time_zone "null"
        UpdatedAt timestamp_with_time_zone "null"
        CreatedBy uuid "null"
        DeletedBy uuid "null"
        UpdatedBy uuid "null"
    }

    tax_rates {
        Id uuid PK "not null"
        IsActive boolean "not null"
        IsDefault boolean "not null"
        IsDeleted boolean "not null"
        RowVersion bytea "not null"
        Name character_varying "not null"
        TaxType integer "not null"
        Rate numeric "not null"
        CreatedAt timestamp_with_time_zone "not null"
        EffectiveFrom timestamp_with_time_zone "not null"
        Description character_varying "null"
        DeletedAt timestamp_with_time_zone "null"
        EffectiveTo timestamp_with_time_zone "null"
        UpdatedAt timestamp_with_time_zone "null"
        CreatedBy uuid "null"
        DeletedBy uuid "null"
        UpdatedBy uuid "null"
    }

    user_addresses {
        Id uuid PK "not null"
        UserId uuid FK "not null"
        IsDefault boolean "not null"
        IsDeleted boolean "not null"
        RowVersion bytea "not null"
        AddressLine1 character_varying "not null"
        City character_varying "not null"
        PhoneNumber character_varying "not null"
        PostalCode character_varying "not null"
        Prefecture character_varying "not null"
        RecipientName character_varying "not null"
        AddressType integer "not null"
        CreatedAt timestamp_with_time_zone "not null"
        AddressLine2 character_varying "null"
        AddressName character_varying "null"
        DeletedAt timestamp_with_time_zone "null"
        UpdatedAt timestamp_with_time_zone "null"
        CreatedBy uuid "null"
        DeletedBy uuid "null"
        UpdatedBy uuid "null"
    }

    user_coupons {
        Id uuid PK "not null"
        CouponId uuid FK "not null"
        UserId uuid FK "not null"
        UsedOrderId uuid FK "null"
        IsDeleted boolean "not null"
        IsUsed boolean "not null"
        RowVersion bytea "not null"
        AcquiredAt timestamp_with_time_zone "not null"
        CreatedAt timestamp_with_time_zone "not null"
        DeletedAt timestamp_with_time_zone "null"
        ExpiresAt timestamp_with_time_zone "null"
        UpdatedAt timestamp_with_time_zone "null"
        UsedAt timestamp_with_time_zone "null"
        CreatedBy uuid "null"
        DeletedBy uuid "null"
        UpdatedBy uuid "null"
    }

    user_histories {
        Id uuid PK "not null"
        OriginalId uuid FK "not null"
        EmailConfirmed boolean "not null"
        IsActive boolean "not null"
        Email character_varying "not null"
        FirstName character_varying "not null"
        LastName character_varying "not null"
        OperationType integer "not null"
        OperatedAt timestamp_with_time_zone "not null"
        OperatedBy uuid "not null"
        PhoneNumber character_varying "null"
        AfterJson text "null"
        BeforeJson text "null"
        ChangeReason text "null"
        IpAddress text "null"
        UserAgent text "null"
        LastLoginAt timestamp_with_time_zone "null"
    }

    user_roles {
        RoleId uuid PK "not null"
        UserId uuid PK "not null"
        RoleId uuid FK "not null"
        UserId uuid FK "not null"
        AssignedAt timestamp_with_time_zone "not null"
        ExpiresAt timestamp_with_time_zone "null"
        AssignedBy uuid "null"
    }

    users {
        Id uuid PK "not null"
        EmailConfirmed boolean "not null"
        IsActive boolean "not null"
        IsDeleted boolean "not null"
        RowVersion bytea "not null"
        Email character_varying "not null"
        FirstName character_varying "not null"
        LastName character_varying "not null"
        PasswordHash text "not null"
        CreatedAt timestamp_with_time_zone "not null"
        PhoneNumber character_varying "null"
        DeletedAt timestamp_with_time_zone "null"
        LastLoginAt timestamp_with_time_zone "null"
        UpdatedAt timestamp_with_time_zone "null"
        CreatedBy uuid "null"
        DeletedBy uuid "null"
        UpdatedBy uuid "null"
    }

    carts ||--o{ cart_items : "cart_items(CartId) -> carts(Id)"
    categories ||--o{ categories : "categories(ParentCategoryId) -> categories(Id)"
    categories ||--o{ coupons : "coupons(TargetCategoryId) -> categories(Id)"
    categories ||--o{ product_categories : "product_categories(CategoryId) -> categories(Id)"
    coupons ||--o{ user_coupons : "user_coupons(CouponId) -> coupons(Id)"
    orders ||--o{ order_histories : "order_histories(OriginalId) -> orders(Id)"
    orders ||--o{ order_items : "order_items(OrderId) -> orders(Id)"
    orders ||--o{ order_status_histories : "order_status_histories(OrderId) -> orders(Id)"
    orders ||--o{ payments : "payments(OrderId) -> orders(Id)"
    orders ||--o{ reviews : "reviews(OrderId) -> orders(Id)"
    orders ||--o{ shippings : "shippings(OrderId) -> orders(Id)"
    orders ||--o{ stock_histories : "stock_histories(RelatedOrderId) -> orders(Id)"
    orders ||--o{ user_coupons : "user_coupons(UsedOrderId) -> orders(Id)"
    payment_methods ||--o{ orders : "orders(PaymentMethodId) -> payment_methods(Id)"
    payment_methods ||--o{ orders : "orders(PaymentMethodId1) -> payment_methods(Id)"
    payment_methods ||--o{ payments : "payments(PaymentMethodId) -> payment_methods(Id)"
    payments ||--o{ payment_histories : "payment_histories(PaymentId) -> payments(Id)"
    products ||--o{ cart_items : "cart_items(ProductId) -> products(Id)"
    products ||--o{ favorites : "favorites(ProductId) -> products(Id)"
    products ||--o{ order_items : "order_items(ProductId) -> products(Id)"
    products ||--o{ product_categories : "product_categories(ProductId) -> products(Id)"
    products ||--o{ product_histories : "product_histories(OriginalId) -> products(Id)"
    products ||--o{ product_images : "product_images(ProductId) -> products(Id)"
    products ||--o{ reviews : "reviews(ProductId) -> products(Id)"
    products ||--o{ stocks : "stocks(ProductId) -> products(Id)"
    roles ||--o{ user_roles : "user_roles(RoleId) -> roles(Id)"
    shipping_methods ||--o{ orders : "orders(ShippingMethodId) -> shipping_methods(Id)"
    shipping_methods ||--o{ orders : "orders(ShippingMethodId1) -> shipping_methods(Id)"
    shipping_methods ||--o{ shippings : "shippings(ShippingMethodId) -> shipping_methods(Id)"
    stocks ||--o{ stock_histories : "stock_histories(StockId) -> stocks(Id)"
    users ||--o{ carts : "carts(UserId) -> users(Id)"
    users ||--o{ favorites : "favorites(UserId) -> users(Id)"
    users ||--o{ orders : "orders(UserId) -> users(Id)"
    users ||--o{ password_reset_tokens : "password_reset_tokens(UserId) -> users(Id)"
    users ||--o{ reviews : "reviews(UserId) -> users(Id)"
    users ||--o{ user_addresses : "user_addresses(UserId) -> users(Id)"
    users ||--o{ user_coupons : "user_coupons(UserId) -> users(Id)"
    users ||--o{ user_histories : "user_histories(OriginalId) -> users(Id)"
    users ||--o{ user_roles : "user_roles(UserId) -> users(Id)"
```

## Indexes

### `__EFMigrationsHistory`

- `PK___EFMigrationsHistory`

### `cart_items`

- `IX_cart_items_CartId_ProductId`
- `IX_cart_items_ProductId`
- `PK_cart_items`

### `carts`

- `IX_carts_LastActivityAt`
- `IX_carts_SessionId`
- `IX_carts_UserId`
- `PK_carts`

### `categories`

- `IX_categories_ParentCategoryId`
- `IX_categories_Slug`
- `PK_categories`

### `coupons`

- `IX_coupons_Code`
- `IX_coupons_TargetCategoryId`
- `IX_coupons_ValidFrom`
- `IX_coupons_ValidTo`
- `PK_coupons`

### `favorites`

- `IX_favorites_AddedAt`
- `IX_favorites_ProductId`
- `IX_favorites_UserId_ProductId`
- `PK_favorites`

### `order_histories`

- `IX_order_histories_OperatedAt`
- `IX_order_histories_OriginalId`
- `PK_order_histories`

### `order_items`

- `IX_order_items_OrderId`
- `IX_order_items_ProductId`
- `PK_order_items`

### `order_status_histories`

- `IX_order_status_histories_CreatedAt`
- `IX_order_status_histories_OrderId`
- `PK_order_status_histories`

### `orders`

- `IX_orders_OrderDate`
- `IX_orders_OrderNumber`
- `IX_orders_PaymentMethodId`
- `IX_orders_PaymentMethodId1`
- `IX_orders_ShippingMethodId`
- `IX_orders_ShippingMethodId1`
- `IX_orders_Status`
- `IX_orders_UserId`
- `PK_orders`

### `password_reset_tokens`

- `IX_password_reset_tokens_TokenHash`
- `IX_password_reset_tokens_UserId_ExpiresAt`
- `PK_password_reset_tokens`

### `payment_histories`

- `IX_payment_histories_OperatedAt`
- `IX_payment_histories_PaymentId`
- `PK_payment_histories`

### `payment_methods`

- `IX_payment_methods_Code`
- `IX_payment_methods_IsActive`
- `PK_payment_methods`

### `payments`

- `IX_payments_OrderId`
- `IX_payments_PaymentMethodId`
- `IX_payments_Status`
- `IX_payments_TransactionId`
- `PK_payments`

### `product_categories`

- `IX_product_categories_CategoryId`
- `PK_product_categories`

### `product_histories`

- `IX_product_histories_OperatedAt`
- `IX_product_histories_OriginalId`
- `PK_product_histories`

### `product_images`

- `IX_product_images_ProductId`
- `PK_product_images`

### `products`

- `IX_products_IsPublished`
- `IX_products_Sku`
- `IX_products_Slug`
- `IX_products_Status`
- `PK_products`

### `reviews`

- `IX_reviews_IsApproved`
- `IX_reviews_OrderId`
- `IX_reviews_ProductId_UserId`
- `IX_reviews_Rating`
- `IX_reviews_UserId`
- `PK_reviews`

### `roles`

- `IX_roles_Name`
- `PK_roles`

### `shipping_methods`

- `IX_shipping_methods_Code`
- `IX_shipping_methods_IsActive`
- `PK_shipping_methods`

### `shippings`

- `IX_shippings_OrderId`
- `IX_shippings_ShippingMethodId`
- `IX_shippings_Status`
- `IX_shippings_TrackingNumber`
- `PK_shippings`

### `stock_histories`

- `IX_stock_histories_MovementType`
- `IX_stock_histories_OperatedAt`
- `IX_stock_histories_RelatedOrderId`
- `IX_stock_histories_StockId`
- `PK_stock_histories`

### `stocks`

- `IX_stocks_ProductId`
- `PK_stocks`

### `system_settings`

- `IX_system_settings_Category`
- `IX_system_settings_Key`
- `PK_system_settings`

### `tax_rates`

- `IX_tax_rates_EffectiveFrom`
- `IX_tax_rates_IsDefault`
- `IX_tax_rates_TaxType`
- `PK_tax_rates`

### `user_addresses`

- `IX_user_addresses_UserId`
- `PK_user_addresses`

### `user_coupons`

- `IX_user_coupons_CouponId`
- `IX_user_coupons_IsUsed`
- `IX_user_coupons_UsedOrderId`
- `IX_user_coupons_UserId_CouponId`
- `PK_user_coupons`

### `user_histories`

- `IX_user_histories_OriginalId`
- `PK_user_histories`

### `user_roles`

- `IX_user_roles_RoleId`
- `PK_user_roles`

### `users`

- `IX_users_Email`
- `PK_users`
