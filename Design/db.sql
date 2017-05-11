USE [master]
GO
/****** Object:  Database [chacd26d_store]    Script Date: 11/05/2017 17:29:00 ******/
CREATE DATABASE [chacd26d_store] ON  PRIMARY 
( NAME = N'chacd26d_store', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\chacd26d_store.mdf' , SIZE = 2304KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'chacd26d_store_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\chacd26d_store_log.LDF' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [chacd26d_store] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [chacd26d_store].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [chacd26d_store] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [chacd26d_store] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [chacd26d_store] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [chacd26d_store] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [chacd26d_store] SET ARITHABORT OFF 
GO
ALTER DATABASE [chacd26d_store] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [chacd26d_store] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [chacd26d_store] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [chacd26d_store] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [chacd26d_store] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [chacd26d_store] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [chacd26d_store] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [chacd26d_store] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [chacd26d_store] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [chacd26d_store] SET  ENABLE_BROKER 
GO
ALTER DATABASE [chacd26d_store] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [chacd26d_store] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [chacd26d_store] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [chacd26d_store] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [chacd26d_store] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [chacd26d_store] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [chacd26d_store] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [chacd26d_store] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [chacd26d_store] SET  MULTI_USER 
GO
ALTER DATABASE [chacd26d_store] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [chacd26d_store] SET DB_CHAINING OFF 
GO
USE [chacd26d_store]
GO
/****** Object:  Table [dbo].[branch]    Script Date: 11/05/2017 17:29:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[branch](
	[id] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](150) NOT NULL,
	[address] [nvarchar](200) NULL,
	[phone] [varchar](15) NULL,
	[hotline] [varchar](15) NULL,
	[num_of_employee] [smallint] NOT NULL,
	[open_day] [datetime] NULL,
	[notes] [nvarchar](255) NULL,
	[location_id] [uniqueidentifier] NULL,
	[create_by] [uniqueidentifier] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[update_by] [uniqueidentifier] NULL,
	[update_date] [datetime] NULL,
	[deleted] [bit] NOT NULL,
	[delete_by] [uniqueidentifier] NULL,
	[delete_date] [datetime] NULL,
 CONSTRAINT [PK_branch] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[branch_employee]    Script Date: 11/05/2017 17:29:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[branch_employee](
	[id] [uniqueidentifier] NOT NULL,
	[branch_id] [uniqueidentifier] NOT NULL,
	[empl_id] [uniqueidentifier] NOT NULL,
	[position_code] [varchar](20) NULL,
	[dept_id] [uniqueidentifier] NULL,
	[start_date] [datetime] NOT NULL,
	[end_date] [datetime] NULL,
	[notes] [nvarchar](255) NULL,
	[create_by] [uniqueidentifier] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[update_by] [uniqueidentifier] NULL,
	[update_date] [datetime] NULL,
	[deleted] [bit] NOT NULL,
	[delete_by] [uniqueidentifier] NULL,
	[delete_date] [datetime] NULL,
 CONSTRAINT [PK_branch_employee] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[contract_type]    Script Date: 11/05/2017 17:29:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[contract_type](
	[code] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_contract_type] PRIMARY KEY CLUSTERED 
(
	[code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[customer]    Script Date: 11/05/2017 17:29:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[customer](
	[id] [uniqueidentifier] NOT NULL,
	[code] [varchar](20) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[birthday] [datetime] NULL,
	[phone] [varchar](15) NULL,
	[email] [varchar](50) NULL,
	[address] [nvarchar](50) NULL,
	[avatar] [varchar](100) NULL,
	[location_id] [uniqueidentifier] NULL,
	[map_position] [varchar](30) NULL,
	[taxcode] [varchar](20) NULL,
	[is_company] [bit] NOT NULL,
	[gender] [bit] NOT NULL,
	[group_id] [uniqueidentifier] NULL,
	[notes] [nvarchar](255) NULL,
	[create_by] [uniqueidentifier] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[update_by] [uniqueidentifier] NULL,
	[update_date] [datetime] NULL,
	[deleted] [bit] NOT NULL,
	[delete_by] [uniqueidentifier] NULL,
	[delete_date] [datetime] NULL,
 CONSTRAINT [PK_customer] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[customer_group]    Script Date: 11/05/2017 17:29:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[customer_group](
	[id] [uniqueidentifier] NOT NULL,
	[code] [varchar](20) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[notes] [nvarchar](255) NULL,
	[create_by] [uniqueidentifier] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[update_by] [uniqueidentifier] NULL,
	[update_date] [datetime] NULL,
	[deleted] [bit] NOT NULL,
	[delete_by] [uniqueidentifier] NULL,
	[delete_date] [datetime] NULL,
 CONSTRAINT [PK_customer_group] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[deliver]    Script Date: 11/05/2017 17:29:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[deliver](
	[id] [uniqueidentifier] NOT NULL,
	[code] [varchar](20) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[phone] [varchar](15) NOT NULL,
	[address] [nvarchar](50) NULL,
	[email] [varchar](50) NULL,
	[is_company] [bit] NOT NULL,
	[group_id] [uniqueidentifier] NULL,
	[notes] [nvarchar](255) NULL,
	[create_by] [uniqueidentifier] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[update_by] [uniqueidentifier] NULL,
	[update_date] [datetime] NULL,
	[deleted] [bit] NOT NULL,
	[delete_by] [uniqueidentifier] NULL,
	[delete_date] [datetime] NULL,
 CONSTRAINT [PK_deliver] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[deliver_group]    Script Date: 11/05/2017 17:29:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[deliver_group](
	[id] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[notes] [nvarchar](255) NULL,
	[create_by] [uniqueidentifier] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[update_by] [uniqueidentifier] NULL,
	[update_date] [datetime] NULL,
	[deleted] [bit] NOT NULL,
	[delete_by] [uniqueidentifier] NULL,
	[delete_date] [datetime] NULL,
 CONSTRAINT [PK_deliver_group] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[department]    Script Date: 11/05/2017 17:29:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[department](
	[id] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_department] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[employee]    Script Date: 11/05/2017 17:29:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[employee](
	[id] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[phone] [varchar](15) NOT NULL,
	[email] [varchar](50) NULL,
	[gender] [bit] NOT NULL,
	[birthday] [datetime] NULL,
	[image] [image] NULL,
	[address] [nvarchar](50) NULL,
	[id_card] [varchar](12) NULL,
	[location_id] [uniqueidentifier] NULL,
	[start_working_date] [datetime] NOT NULL,
	[end_working_date] [datetime] NULL,
	[contract_type_code] [uniqueidentifier] NOT NULL,
	[create_by] [uniqueidentifier] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[update_by] [uniqueidentifier] NULL,
	[update_date] [datetime] NULL,
	[deleted] [bit] NOT NULL,
	[delete_by] [uniqueidentifier] NULL,
	[delete_date] [datetime] NULL,
 CONSTRAINT [PK_employee] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[function]    Script Date: 11/05/2017 17:29:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[function](
	[code] [varchar](20) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[action] [varchar](20) NOT NULL,
	[controller] [varchar](20) NOT NULL,
	[area] [varchar](30) NULL,
 CONSTRAINT [PK_function] PRIMARY KEY CLUSTERED 
(
	[code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[inventory]    Script Date: 11/05/2017 17:29:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[inventory](
	[id] [uniqueidentifier] NOT NULL,
	[branch_id] [uniqueidentifier] NOT NULL,
	[product_id] [uniqueidentifier] NOT NULL,
	[total] [float] NOT NULL,
	[last_update] [datetime] NOT NULL,
 CONSTRAINT [PK_inventory] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[leave]    Script Date: 11/05/2017 17:29:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[leave](
	[id] [uniqueidentifier] NOT NULL,
	[title] [nvarchar](200) NOT NULL,
	[user_id] [uniqueidentifier] NOT NULL,
	[start_date] [datetime] NOT NULL,
	[end_date] [datetime] NOT NULL,
	[duration] [smallint] NOT NULL,
	[leave_type_id] [uniqueidentifier] NOT NULL,
	[assignee] [uniqueidentifier] NOT NULL,
	[comment] [nvarchar](255) NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_leave] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[leave_type]    Script Date: 11/05/2017 17:29:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[leave_type](
	[id] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_leave_type] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[location]    Script Date: 11/05/2017 17:29:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[location](
	[id] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_location] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[position]    Script Date: 11/05/2017 17:29:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[position](
	[code] [varchar](20) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_position] PRIMARY KEY CLUSTERED 
(
	[code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[product]    Script Date: 11/05/2017 17:29:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[product](
	[id] [uniqueidentifier] NOT NULL,
	[code] [varchar](20) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[type_id] [uniqueidentifier] NOT NULL,
	[unit_id] [uniqueidentifier] NULL,
	[group_id] [uniqueidentifier] NULL,
	[price] [money] NULL,
	[original_price] [money] NULL,
	[number_in_stock] [decimal](7, 1) NULL,
	[weight] [decimal](7, 1) NULL,
	[allow_sale_direct] [bit] NOT NULL,
	[pic01] [nvarchar](50) NULL,
	[pic02] [nvarchar](50) NULL,
	[pic03] [nvarchar](50) NULL,
	[pic04] [nvarchar](50) NULL,
	[pic05] [nvarchar](50) NULL,
	[min_in_stock] [decimal](7, 1) NULL,
	[max_in_stock] [decimal](7, 1) NULL,
	[description] [nvarchar](max) NULL,
	[note_in_order] [nvarchar](255) NULL,
	[create_by] [uniqueidentifier] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[update_by] [uniqueidentifier] NULL,
	[update_date] [datetime] NULL,
	[deleted] [bit] NOT NULL,
	[delete_by] [uniqueidentifier] NULL,
	[delete_date] [datetime] NULL,
 CONSTRAINT [PK_product] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[product_group]    Script Date: 11/05/2017 17:29:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product_group](
	[id] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[notes] [nvarchar](255) NULL,
	[create_by] [uniqueidentifier] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[update_by] [uniqueidentifier] NULL,
	[update_date] [datetime] NULL,
	[deleted] [bit] NOT NULL,
	[delete_by] [uniqueidentifier] NULL,
	[delete_date] [datetime] NULL,
 CONSTRAINT [PK_product_group] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[product_type]    Script Date: 11/05/2017 17:29:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product_type](
	[id] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[notes] [nvarchar](255) NULL,
	[create_by] [uniqueidentifier] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[update_by] [uniqueidentifier] NULL,
	[update_date] [datetime] NULL,
	[deleted] [bit] NOT NULL,
	[delete_by] [uniqueidentifier] NULL,
	[delete_date] [datetime] NULL,
 CONSTRAINT [PK_product_type] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[reset_password_history]    Script Date: 11/05/2017 17:29:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[reset_password_history](
	[id] [uniqueidentifier] NOT NULL,
	[user_require] [uniqueidentifier] NOT NULL,
	[require_date] [datetime] NOT NULL,
	[status] [tinyint] NULL,
	[code] [varchar](10) NULL,
	[token] [varchar](100) NULL,
	[reset_date] [datetime] NULL,
	[expried_date] [datetime] NULL,
 CONSTRAINT [PK_user_reset_password_history] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[role]    Script Date: 11/05/2017 17:29:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[role](
	[id] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[notes] [nvarchar](255) NULL,
	[create_by] [uniqueidentifier] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[update_by] [uniqueidentifier] NULL,
	[update_date] [datetime] NULL,
	[deleted] [bit] NOT NULL,
	[delete_by] [uniqueidentifier] NULL,
	[delete_date] [datetime] NULL,
 CONSTRAINT [PK_role] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[role_detail]    Script Date: 11/05/2017 17:29:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[role_detail](
	[id] [uniqueidentifier] NOT NULL,
	[role_id] [uniqueidentifier] NOT NULL,
	[function_code] [varchar](20) NOT NULL,
 CONSTRAINT [PK_role_detail] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[stock_in]    Script Date: 11/05/2017 17:29:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[stock_in](
	[id] [uniqueidentifier] NOT NULL,
	[code] [varchar](20) NOT NULL,
	[supplier_id] [uniqueidentifier] NOT NULL,
	[stock_in_date] [datetime] NOT NULL,
	[total_money] [money] NOT NULL,
	[discount] [money] NOT NULL,
	[payable] [money] NOT NULL,
	[dept] [money] NOT NULL,
	[notes] [nvarchar](255) NULL,
	[create_by] [uniqueidentifier] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[update_by] [uniqueidentifier] NULL,
	[update_date] [datetime] NULL,
	[deleted] [bit] NOT NULL,
	[delete_by] [uniqueidentifier] NULL,
	[delete_date] [datetime] NULL,
 CONSTRAINT [PK_stock_in] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[stock_in_detail]    Script Date: 11/05/2017 17:29:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[stock_in_detail](
	[id] [uniqueidentifier] NOT NULL,
	[stock_in_id] [uniqueidentifier] NOT NULL,
	[product_id] [uniqueidentifier] NOT NULL,
	[number] [decimal](5, 1) NOT NULL,
	[price] [money] NOT NULL,
 CONSTRAINT [PK_stock_in_detail] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[stock_out]    Script Date: 11/05/2017 17:29:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[stock_out](
	[id] [uniqueidentifier] NOT NULL,
	[code] [varchar](20) NOT NULL,
	[stock_out_date] [datetime] NOT NULL,
	[notes] [nvarchar](255) NULL,
	[create_by] [uniqueidentifier] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[update_by] [uniqueidentifier] NULL,
	[update_date] [datetime] NULL,
	[deleted] [bit] NOT NULL,
	[delete_by] [uniqueidentifier] NULL,
	[delete_date] [datetime] NULL,
 CONSTRAINT [PK_stock_out] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[supplier]    Script Date: 11/05/2017 17:29:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[supplier](
	[id] [uniqueidentifier] NOT NULL,
	[code] [varchar](20) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[avatar] [varchar](100) NULL,
	[phone] [varchar](15) NOT NULL,
	[address] [nvarchar](50) NULL,
	[email] [varchar](50) NULL,
	[group_id] [uniqueidentifier] NULL,
	[taxcode] [varchar](25) NULL,
	[company_name] [nvarchar](50) NULL,
	[notes] [nvarchar](255) NULL,
	[create_by] [uniqueidentifier] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[update_by] [uniqueidentifier] NULL,
	[update_date] [datetime] NULL,
	[deleted] [bit] NOT NULL,
	[delete_by] [uniqueidentifier] NULL,
	[delete_date] [datetime] NULL,
 CONSTRAINT [PK_supplier] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[supplier_group]    Script Date: 11/05/2017 17:29:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[supplier_group](
	[id] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[notes] [nvarchar](255) NULL,
	[create_by] [uniqueidentifier] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[update_by] [uniqueidentifier] NULL,
	[update_date] [datetime] NULL,
	[deleted] [bit] NOT NULL,
	[delete_by] [uniqueidentifier] NULL,
	[delete_date] [datetime] NULL,
 CONSTRAINT [PK_supplier_group] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[token]    Script Date: 11/05/2017 17:29:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[token](
	[id] [uniqueidentifier] NOT NULL,
	[user_id] [uniqueidentifier] NOT NULL,
	[auth_token] [nvarchar](250) NOT NULL,
	[issued_on] [datetime] NOT NULL,
	[expires_on] [datetime] NOT NULL,
 CONSTRAINT [PK_token] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[unit]    Script Date: 11/05/2017 17:29:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[unit](
	[id] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[notes] [nvarchar](255) NULL,
	[create_by] [uniqueidentifier] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[update_by] [uniqueidentifier] NULL,
	[update_date] [datetime] NULL,
	[deleted] [bit] NOT NULL,
	[delete_by] [uniqueidentifier] NULL,
	[delete_date] [datetime] NULL,
 CONSTRAINT [PK_unit] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[user]    Script Date: 11/05/2017 17:29:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[user](
	[id] [uniqueidentifier] NOT NULL,
	[user_name] [varchar](20) NOT NULL,
	[password] [nvarchar](100) NOT NULL,
	[empl_id] [uniqueidentifier] NOT NULL,
	[locked] [bit] NOT NULL,
	[role_id] [uniqueidentifier] NOT NULL,
	[create_by] [uniqueidentifier] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[update_by] [uniqueidentifier] NULL,
	[update_date] [datetime] NULL,
	[deleted] [bit] NOT NULL,
	[delete_by] [uniqueidentifier] NULL,
	[delete_date] [datetime] NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[branch] ADD  CONSTRAINT [DF_branch_num_of_employee]  DEFAULT ((0)) FOR [num_of_employee]
GO
ALTER TABLE [dbo].[branch] ADD  CONSTRAINT [DF_branch_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[branch] ADD  CONSTRAINT [DF_branch_deleted]  DEFAULT ((0)) FOR [deleted]
GO
ALTER TABLE [dbo].[branch_employee] ADD  CONSTRAINT [DF_branch_employee_start_date]  DEFAULT (getdate()) FOR [start_date]
GO
ALTER TABLE [dbo].[branch_employee] ADD  CONSTRAINT [DF_branch_employee_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[branch_employee] ADD  CONSTRAINT [DF_branch_employee_deleted]  DEFAULT ((0)) FOR [deleted]
GO
ALTER TABLE [dbo].[customer] ADD  CONSTRAINT [DF_customer_is_company]  DEFAULT ((0)) FOR [is_company]
GO
ALTER TABLE [dbo].[customer] ADD  CONSTRAINT [DF_customer_gender]  DEFAULT ((0)) FOR [gender]
GO
ALTER TABLE [dbo].[customer] ADD  CONSTRAINT [DF_customer_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[customer] ADD  CONSTRAINT [DF_customer_deleted]  DEFAULT ((0)) FOR [deleted]
GO
ALTER TABLE [dbo].[customer_group] ADD  CONSTRAINT [DF_customer_group_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[customer_group] ADD  CONSTRAINT [DF_customer_group_deleted]  DEFAULT ((0)) FOR [deleted]
GO
ALTER TABLE [dbo].[deliver] ADD  CONSTRAINT [DF_deliver_is_company]  DEFAULT ((0)) FOR [is_company]
GO
ALTER TABLE [dbo].[deliver] ADD  CONSTRAINT [DF_deliver_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[deliver] ADD  CONSTRAINT [DF_deliver_deleted]  DEFAULT ((0)) FOR [deleted]
GO
ALTER TABLE [dbo].[deliver_group] ADD  CONSTRAINT [DF_deliver_group_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[deliver_group] ADD  CONSTRAINT [DF_deliver_group_deleted]  DEFAULT ((0)) FOR [deleted]
GO
ALTER TABLE [dbo].[employee] ADD  CONSTRAINT [DF_employee_gender]  DEFAULT ((0)) FOR [gender]
GO
ALTER TABLE [dbo].[employee] ADD  CONSTRAINT [DF_employee_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[employee] ADD  CONSTRAINT [DF_employee_deleted]  DEFAULT ((0)) FOR [deleted]
GO
ALTER TABLE [dbo].[inventory] ADD  CONSTRAINT [DF_inventory_total]  DEFAULT ((0)) FOR [total]
GO
ALTER TABLE [dbo].[inventory] ADD  CONSTRAINT [DF_inventory_last_update]  DEFAULT (getdate()) FOR [last_update]
GO
ALTER TABLE [dbo].[leave] ADD  CONSTRAINT [DF_leave_duration]  DEFAULT ((0)) FOR [duration]
GO
ALTER TABLE [dbo].[leave] ADD  CONSTRAINT [DF_leave_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[product] ADD  CONSTRAINT [DF_product_allow_sale_direct]  DEFAULT ((1)) FOR [allow_sale_direct]
GO
ALTER TABLE [dbo].[product] ADD  CONSTRAINT [DF_product_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[product] ADD  CONSTRAINT [DF_product_deleted]  DEFAULT ((0)) FOR [deleted]
GO
ALTER TABLE [dbo].[product_group] ADD  CONSTRAINT [DF_product_group_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[product_group] ADD  CONSTRAINT [DF_product_group_deleted]  DEFAULT ((0)) FOR [deleted]
GO
ALTER TABLE [dbo].[product_type] ADD  CONSTRAINT [DF_product_type_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[product_type] ADD  CONSTRAINT [DF_product_type_deleted]  DEFAULT ((0)) FOR [deleted]
GO
ALTER TABLE [dbo].[reset_password_history] ADD  CONSTRAINT [DF_user_reset_password_history_require_date]  DEFAULT (getdate()) FOR [require_date]
GO
ALTER TABLE [dbo].[reset_password_history] ADD  CONSTRAINT [DF_user_reset_password_history_status]  DEFAULT ((0)) FOR [status]
GO
ALTER TABLE [dbo].[reset_password_history] ADD  CONSTRAINT [DF_reset_password_history_expried_date]  DEFAULT (getdate()) FOR [expried_date]
GO
ALTER TABLE [dbo].[role] ADD  CONSTRAINT [DF_role_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[role] ADD  CONSTRAINT [DF_role_deleted]  DEFAULT ((0)) FOR [deleted]
GO
ALTER TABLE [dbo].[stock_in] ADD  CONSTRAINT [DF_stock_in_dept]  DEFAULT ((0)) FOR [dept]
GO
ALTER TABLE [dbo].[stock_in] ADD  CONSTRAINT [DF_stock_in_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[stock_in] ADD  CONSTRAINT [DF_stock_in_deleted]  DEFAULT ((0)) FOR [deleted]
GO
ALTER TABLE [dbo].[stock_in_detail] ADD  CONSTRAINT [DF_stock_in_detail_number]  DEFAULT ((0)) FOR [number]
GO
ALTER TABLE [dbo].[stock_in_detail] ADD  CONSTRAINT [DF_stock_in_detail_price]  DEFAULT ((1)) FOR [price]
GO
ALTER TABLE [dbo].[stock_out] ADD  CONSTRAINT [DF_stock_out_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[stock_out] ADD  CONSTRAINT [DF_stock_out_deleted]  DEFAULT ((0)) FOR [deleted]
GO
ALTER TABLE [dbo].[supplier] ADD  CONSTRAINT [DF_supplier_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[supplier] ADD  CONSTRAINT [DF_supplier_deleted]  DEFAULT ((0)) FOR [deleted]
GO
ALTER TABLE [dbo].[supplier_group] ADD  CONSTRAINT [DF_supplier_group_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[supplier_group] ADD  CONSTRAINT [DF_supplier_group_deleted]  DEFAULT ((0)) FOR [deleted]
GO
ALTER TABLE [dbo].[unit] ADD  CONSTRAINT [DF_unit_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[unit] ADD  CONSTRAINT [DF_unit_deleted]  DEFAULT ((0)) FOR [deleted]
GO
ALTER TABLE [dbo].[user] ADD  CONSTRAINT [DF_user_locked]  DEFAULT ((0)) FOR [locked]
GO
ALTER TABLE [dbo].[user] ADD  CONSTRAINT [DF_user_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[user] ADD  CONSTRAINT [DF_user_deleted]  DEFAULT ((0)) FOR [deleted]
GO
ALTER TABLE [dbo].[branch]  WITH CHECK ADD  CONSTRAINT [FK_branch_location] FOREIGN KEY([location_id])
REFERENCES [dbo].[location] ([id])
GO
ALTER TABLE [dbo].[branch] CHECK CONSTRAINT [FK_branch_location]
GO
ALTER TABLE [dbo].[branch]  WITH CHECK ADD  CONSTRAINT [FK_branch_user] FOREIGN KEY([create_by])
REFERENCES [dbo].[user] ([id])
GO
ALTER TABLE [dbo].[branch] CHECK CONSTRAINT [FK_branch_user]
GO
ALTER TABLE [dbo].[branch_employee]  WITH CHECK ADD  CONSTRAINT [FK_branch_employee_branch_employee] FOREIGN KEY([branch_id])
REFERENCES [dbo].[branch] ([id])
GO
ALTER TABLE [dbo].[branch_employee] CHECK CONSTRAINT [FK_branch_employee_branch_employee]
GO
ALTER TABLE [dbo].[branch_employee]  WITH CHECK ADD  CONSTRAINT [FK_branch_employee_department] FOREIGN KEY([dept_id])
REFERENCES [dbo].[department] ([id])
GO
ALTER TABLE [dbo].[branch_employee] CHECK CONSTRAINT [FK_branch_employee_department]
GO
ALTER TABLE [dbo].[branch_employee]  WITH CHECK ADD  CONSTRAINT [FK_branch_employee_employee] FOREIGN KEY([empl_id])
REFERENCES [dbo].[employee] ([id])
GO
ALTER TABLE [dbo].[branch_employee] CHECK CONSTRAINT [FK_branch_employee_employee]
GO
ALTER TABLE [dbo].[branch_employee]  WITH CHECK ADD  CONSTRAINT [FK_branch_employee_position] FOREIGN KEY([position_code])
REFERENCES [dbo].[position] ([code])
GO
ALTER TABLE [dbo].[branch_employee] CHECK CONSTRAINT [FK_branch_employee_position]
GO
ALTER TABLE [dbo].[customer]  WITH CHECK ADD  CONSTRAINT [FK_customer_customer_group] FOREIGN KEY([group_id])
REFERENCES [dbo].[customer_group] ([id])
GO
ALTER TABLE [dbo].[customer] CHECK CONSTRAINT [FK_customer_customer_group]
GO
ALTER TABLE [dbo].[customer]  WITH CHECK ADD  CONSTRAINT [FK_customer_location] FOREIGN KEY([location_id])
REFERENCES [dbo].[location] ([id])
GO
ALTER TABLE [dbo].[customer] CHECK CONSTRAINT [FK_customer_location]
GO
ALTER TABLE [dbo].[deliver]  WITH CHECK ADD  CONSTRAINT [FK_deliver_deliver_group] FOREIGN KEY([group_id])
REFERENCES [dbo].[deliver_group] ([id])
GO
ALTER TABLE [dbo].[deliver] CHECK CONSTRAINT [FK_deliver_deliver_group]
GO
ALTER TABLE [dbo].[employee]  WITH CHECK ADD  CONSTRAINT [FK_employee_contract_type] FOREIGN KEY([contract_type_code])
REFERENCES [dbo].[contract_type] ([code])
GO
ALTER TABLE [dbo].[employee] CHECK CONSTRAINT [FK_employee_contract_type]
GO
ALTER TABLE [dbo].[employee]  WITH CHECK ADD  CONSTRAINT [FK_employee_location] FOREIGN KEY([location_id])
REFERENCES [dbo].[location] ([id])
GO
ALTER TABLE [dbo].[employee] CHECK CONSTRAINT [FK_employee_location]
GO
ALTER TABLE [dbo].[inventory]  WITH CHECK ADD  CONSTRAINT [FK_inventory_branch] FOREIGN KEY([branch_id])
REFERENCES [dbo].[branch] ([id])
GO
ALTER TABLE [dbo].[inventory] CHECK CONSTRAINT [FK_inventory_branch]
GO
ALTER TABLE [dbo].[leave]  WITH CHECK ADD  CONSTRAINT [FK_leave_leave_type] FOREIGN KEY([leave_type_id])
REFERENCES [dbo].[leave_type] ([id])
GO
ALTER TABLE [dbo].[leave] CHECK CONSTRAINT [FK_leave_leave_type]
GO
ALTER TABLE [dbo].[leave]  WITH CHECK ADD  CONSTRAINT [FK_leave_user] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([id])
GO
ALTER TABLE [dbo].[leave] CHECK CONSTRAINT [FK_leave_user]
GO
ALTER TABLE [dbo].[leave]  WITH CHECK ADD  CONSTRAINT [FK_leave_user1] FOREIGN KEY([assignee])
REFERENCES [dbo].[user] ([id])
GO
ALTER TABLE [dbo].[leave] CHECK CONSTRAINT [FK_leave_user1]
GO
ALTER TABLE [dbo].[product]  WITH CHECK ADD  CONSTRAINT [FK_product_product_group] FOREIGN KEY([group_id])
REFERENCES [dbo].[product_group] ([id])
GO
ALTER TABLE [dbo].[product] CHECK CONSTRAINT [FK_product_product_group]
GO
ALTER TABLE [dbo].[product]  WITH CHECK ADD  CONSTRAINT [FK_product_product_type] FOREIGN KEY([type_id])
REFERENCES [dbo].[product_type] ([id])
GO
ALTER TABLE [dbo].[product] CHECK CONSTRAINT [FK_product_product_type]
GO
ALTER TABLE [dbo].[product]  WITH CHECK ADD  CONSTRAINT [FK_product_unit] FOREIGN KEY([unit_id])
REFERENCES [dbo].[unit] ([id])
GO
ALTER TABLE [dbo].[product] CHECK CONSTRAINT [FK_product_unit]
GO
ALTER TABLE [dbo].[reset_password_history]  WITH CHECK ADD  CONSTRAINT [FK_reset_password_history_user] FOREIGN KEY([user_require])
REFERENCES [dbo].[user] ([id])
GO
ALTER TABLE [dbo].[reset_password_history] CHECK CONSTRAINT [FK_reset_password_history_user]
GO
ALTER TABLE [dbo].[role_detail]  WITH CHECK ADD  CONSTRAINT [FK_role_detail_function] FOREIGN KEY([function_code])
REFERENCES [dbo].[function] ([code])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[role_detail] CHECK CONSTRAINT [FK_role_detail_function]
GO
ALTER TABLE [dbo].[role_detail]  WITH CHECK ADD  CONSTRAINT [FK_role_detail_role] FOREIGN KEY([role_id])
REFERENCES [dbo].[role] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[role_detail] CHECK CONSTRAINT [FK_role_detail_role]
GO
ALTER TABLE [dbo].[stock_in]  WITH CHECK ADD  CONSTRAINT [FK_stock_in_supplier] FOREIGN KEY([supplier_id])
REFERENCES [dbo].[supplier] ([id])
GO
ALTER TABLE [dbo].[stock_in] CHECK CONSTRAINT [FK_stock_in_supplier]
GO
ALTER TABLE [dbo].[stock_in_detail]  WITH CHECK ADD  CONSTRAINT [FK_stock_in_detail_product] FOREIGN KEY([product_id])
REFERENCES [dbo].[product] ([id])
GO
ALTER TABLE [dbo].[stock_in_detail] CHECK CONSTRAINT [FK_stock_in_detail_product]
GO
ALTER TABLE [dbo].[stock_in_detail]  WITH CHECK ADD  CONSTRAINT [FK_stock_in_detail_stock_in] FOREIGN KEY([stock_in_id])
REFERENCES [dbo].[stock_in] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[stock_in_detail] CHECK CONSTRAINT [FK_stock_in_detail_stock_in]
GO
ALTER TABLE [dbo].[supplier]  WITH CHECK ADD  CONSTRAINT [FK_supplier_supplier_group] FOREIGN KEY([group_id])
REFERENCES [dbo].[supplier_group] ([id])
GO
ALTER TABLE [dbo].[supplier] CHECK CONSTRAINT [FK_supplier_supplier_group]
GO
ALTER TABLE [dbo].[token]  WITH CHECK ADD  CONSTRAINT [FK_token_user] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([id])
GO
ALTER TABLE [dbo].[token] CHECK CONSTRAINT [FK_token_user]
GO
ALTER TABLE [dbo].[user]  WITH CHECK ADD  CONSTRAINT [FK_user_employee] FOREIGN KEY([empl_id])
REFERENCES [dbo].[employee] ([id])
GO
ALTER TABLE [dbo].[user] CHECK CONSTRAINT [FK_user_employee]
GO
USE [master]
GO
ALTER DATABASE [chacd26d_store] SET  READ_WRITE 
GO
