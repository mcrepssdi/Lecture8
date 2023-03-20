USE [Lab1]
GO
/****** Object:  StoredProcedure [dbo].[spEnergyConsumption]    Script Date: 3/20/2023 5:28:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spEnergyConsumption]
	@State nvarchar(2)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT * FROM dbo.EnergyConsumption WHERE State = @State;
END
