-- DROP SCHEMA dbo;

create Database belanjayuk; 

CREATE TABLE belanjayuk.dbo.LtCategory (
	IdCategory nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CategoryName nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DateIn datetime NULL,
	DateUp datetime NULL,
	UserIn nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	UserUp nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	IsActive bit NULL,
	CONSTRAINT PK_LtCategory PRIMARY KEY (IdCategory)
);


-- belanjayuk.dbo.LtGender definition

-- Drop table

-- DROP TABLE belanjayuk.dbo.LtGender;

CREATE TABLE belanjayuk.dbo.LtGender (
	IdGender nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	GenderName nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DateIn datetime NULL,
	UserIn nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DateUp datetime NULL,
	UserUp nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	IsActive bit NULL,
	CONSTRAINT PK_LtGender PRIMARY KEY (IdGender)
);


-- belanjayuk.dbo.LtPayment definition

-- Drop table

-- DROP TABLE belanjayuk.dbo.LtPayment;

CREATE TABLE belanjayuk.dbo.LtPayment (
	IdPayment nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	PaymentName nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DateIn datetime NULL,
	DateUp datetime NULL,
	UserIn nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	UserUp nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	IsActive bit NULL,
	CONSTRAINT PK_LtPayment PRIMARY KEY (IdPayment)
);


-- belanjayuk.dbo.MsUser definition

-- Drop table

-- DROP TABLE belanjayuk.dbo.MsUser;

CREATE TABLE belanjayuk.dbo.MsUser (
	IdUser nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	UserName nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Email nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	PhoneNumber nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	FirstName nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	LastName nvarchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DOB datetime NULL,
	DateIn datetime NULL,
	UserIn nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DateUp datetime NULL,
	UserUp nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	IsActive bit NULL,
	IdGender nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK_MsUser PRIMARY KEY (IdUser),
	CONSTRAINT FK_MsUser_LtGender FOREIGN KEY (IdGender) REFERENCES belanjayuk.dbo.LtGender(IdGender)
);
 CREATE NONCLUSTERED INDEX IX_MsUser_IdGender ON belanjayuk.dbo.MsUser (  IdGender ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;


-- belanjayuk.dbo.MsUserPassword definition

-- Drop table

-- DROP TABLE belanjayuk.dbo.MsUserPassword;

CREATE TABLE belanjayuk.dbo.MsUserPassword (
	IdUserPassword nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	IdUser nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	PasswordHashed nvarchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DateIn datetime NULL,
	DateUp datetime NULL,
	UserIn nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	UserUp nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	IsActive bit NULL,
	CONSTRAINT PK_MsUserPassword PRIMARY KEY (IdUserPassword),
	CONSTRAINT FK_MsUserPassword_MsUser FOREIGN KEY (IdUser) REFERENCES belanjayuk.dbo.MsUser(IdUser)
);
 CREATE NONCLUSTERED INDEX IX_MsUserPassword_IdUser ON belanjayuk.dbo.MsUserPassword (  IdUser ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;


-- belanjayuk.dbo.MsUserSeller definition

-- Drop table

-- DROP TABLE belanjayuk.dbo.MsUserSeller;

CREATE TABLE belanjayuk.dbo.MsUserSeller (
	IdUserSeller nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	IdUser nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	StoreName nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	SellerDesc nvarchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Address nvarchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	PhoneNumber nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Email nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DateIn datetime NULL,
	DateUp datetime NULL,
	UserIn nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	UserUp nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	IsActive bit NULL,
	CONSTRAINT PK_MsUserSeller PRIMARY KEY (IdUserSeller),
	CONSTRAINT FK_MsUserSeller_MsUser FOREIGN KEY (IdUser) REFERENCES belanjayuk.dbo.MsUser(IdUser)
);
 CREATE NONCLUSTERED INDEX IX_MsUserSeller_IdUser ON belanjayuk.dbo.MsUserSeller (  IdUser ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;


-- belanjayuk.dbo.TrBuyerTransaction definition

-- Drop table

-- DROP TABLE belanjayuk.dbo.TrBuyerTransaction;

CREATE TABLE belanjayuk.dbo.TrBuyerTransaction (
	IdBuyerTransaction nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	IdUser nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	IdPayment nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	FinalPrice decimal(18,2) NULL,
	Rating int NULL,
	RatingComment nvarchar(1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DateIn datetime NULL,
	DateUp datetime NULL,
	UserIn nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	UserUp nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	IsActive bit NULL,
	CONSTRAINT PK_TrBuyerTransaction PRIMARY KEY (IdBuyerTransaction),
	CONSTRAINT FK_TrBuyerTransaction_LtPayment FOREIGN KEY (IdPayment) REFERENCES belanjayuk.dbo.LtPayment(IdPayment),
	CONSTRAINT FK_TrBuyerTransaction_MsUser FOREIGN KEY (IdUser) REFERENCES belanjayuk.dbo.MsUser(IdUser)
);
 CREATE NONCLUSTERED INDEX IX_TrBuyerTransaction_IdPayment ON belanjayuk.dbo.TrBuyerTransaction (  IdPayment ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;
 CREATE NONCLUSTERED INDEX IX_TrBuyerTransaction_IdUser ON belanjayuk.dbo.TrBuyerTransaction (  IdUser ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;


-- belanjayuk.dbo.TrHomeAddress definition

-- Drop table

-- DROP TABLE belanjayuk.dbo.TrHomeAddress;

CREATE TABLE belanjayuk.dbo.TrHomeAddress (
	IdHomeAddress nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	IdUser nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Provinsi nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	KotaKabupaten nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Kecamatan nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	KodePos nvarchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	HomeAddressDesc nvarchar(2000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	IsPrimaryAddress bit NULL,
	DateIn datetime NULL,
	DateUp datetime NULL,
	UserIn nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	UserUp nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	IsActive bit NULL,
	CONSTRAINT PK_TrHomeAddress PRIMARY KEY (IdHomeAddress),
	CONSTRAINT FK_TrHomeAddress_MsUser FOREIGN KEY (IdUser) REFERENCES belanjayuk.dbo.MsUser(IdUser)
);
 CREATE NONCLUSTERED INDEX IX_TrHomeAddress_IdUser ON belanjayuk.dbo.TrHomeAddress (  IdUser ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;


-- belanjayuk.dbo.MsProduct definition

-- Drop table

-- DROP TABLE belanjayuk.dbo.MsProduct;

CREATE TABLE belanjayuk.dbo.MsProduct (
	IdProduct nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	IdUserSeller nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ProductName nvarchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ProductDesc nvarchar(2000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Price decimal(18,2) NULL,
	Discount decimal(18,2) NULL,
	IdCategory nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DateIn datetime NULL,
	DateUp datetime NULL,
	UserIn nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	UserUp nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	IsActive bit NULL,
	CONSTRAINT PK_MsProduct PRIMARY KEY (IdProduct),
	CONSTRAINT FK_MsProduct_LtCategory FOREIGN KEY (IdCategory) REFERENCES belanjayuk.dbo.LtCategory(IdCategory),
	CONSTRAINT FK_MsProduct_MsUserSeller FOREIGN KEY (IdUserSeller) REFERENCES belanjayuk.dbo.MsUserSeller(IdUserSeller)
);
 CREATE NONCLUSTERED INDEX IX_MsProduct_IdCategory ON belanjayuk.dbo.MsProduct (  IdCategory ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;
 CREATE NONCLUSTERED INDEX IX_MsProduct_IdUserSeller ON belanjayuk.dbo.MsProduct (  IdUserSeller ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;


-- belanjayuk.dbo.TrBuyerCart definition

-- Drop table

-- DROP TABLE belanjayuk.dbo.TrBuyerCart;

CREATE TABLE belanjayuk.dbo.TrBuyerCart (
	IdBuyerCart nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	IdUser nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	IdProduct nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Qty int NULL,
	DateIn datetime NULL,
	DateUp datetime NULL,
	UserIn nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	UserUp nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	IsActive bit NULL,
	CONSTRAINT PK_TrBuyerCart PRIMARY KEY (IdBuyerCart),
	CONSTRAINT FK_TrBuyerCart_MsProduct FOREIGN KEY (IdProduct) REFERENCES belanjayuk.dbo.MsProduct(IdProduct),
	CONSTRAINT FK_TrBuyerCart_MsUser FOREIGN KEY (IdUser) REFERENCES belanjayuk.dbo.MsUser(IdUser)
);
 CREATE NONCLUSTERED INDEX IX_TrBuyerCart_IdProduct ON belanjayuk.dbo.TrBuyerCart (  IdProduct ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;
 CREATE NONCLUSTERED INDEX IX_TrBuyerCart_IdUser ON belanjayuk.dbo.TrBuyerCart (  IdUser ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;


-- belanjayuk.dbo.TrBuyerTransactionDetail definition

-- Drop table

-- DROP TABLE belanjayuk.dbo.TrBuyerTransactionDetail;

CREATE TABLE belanjayuk.dbo.TrBuyerTransactionDetail (
	IdBuyerTransactionDetail nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	IdBuyerTransaction nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	IdProduct nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Qty int NULL,
	PriceOfProduct decimal(18,2) NULL,
	DiscountProduct decimal(18,2) NULL,
	Rating int NULL,
	RatingComment nvarchar(1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	UserIn nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	UserUp nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DateIn datetime NULL,
	DateUp datetime NULL,
	IsActive bit NULL,
	CONSTRAINT PK_TrBuyerTransactionDetail PRIMARY KEY (IdBuyerTransactionDetail),
	CONSTRAINT FK_TrBTD_MsProduct FOREIGN KEY (IdProduct) REFERENCES belanjayuk.dbo.MsProduct(IdProduct),
	CONSTRAINT FK_TrBTD_TrBuyerTransaction FOREIGN KEY (IdBuyerTransaction) REFERENCES belanjayuk.dbo.TrBuyerTransaction(IdBuyerTransaction)
);
 CREATE NONCLUSTERED INDEX IX_TrBTD_IdBuyerTransaction ON belanjayuk.dbo.TrBuyerTransactionDetail (  IdBuyerTransaction ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;
 CREATE NONCLUSTERED INDEX IX_TrBTD_IdProduct ON belanjayuk.dbo.TrBuyerTransactionDetail (  IdProduct ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;


-- belanjayuk.dbo.TrProductImages definition

-- Drop table

-- DROP TABLE belanjayuk.dbo.TrProductImages;

CREATE TABLE belanjayuk.dbo.TrProductImages (
	IdProductImages nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	IdProduct nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	ProductImage nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	DateIn datetime NULL,
	DateUp datetime NULL,
	UserIn nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	UserUp nvarchar(36) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	IsActive bit NULL,
	CONSTRAINT PK_TrProductImages PRIMARY KEY (IdProductImages),
	CONSTRAINT FK_TrProductImages_MsProduct FOREIGN KEY (IdProduct) REFERENCES belanjayuk.dbo.MsProduct(IdProduct)
);
 CREATE NONCLUSTERED INDEX IX_TrProductImages_IdProduct ON belanjayuk.dbo.TrProductImages (  IdProduct ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;


use belanjayuk;

