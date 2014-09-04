-- TelerikMovieDatabase.Data.MySql.GrossReport
CREATE TABLE `GrossReports` (
    `ReportID` integer AUTO_INCREMENT NOT NULL, -- _idGrossReports
    `Title` nvarchar(45) NOT NULL,          -- _title
    `Gross` integer NOT NULL,               -- _gross
    CONSTRAINT `pk_GrossReports` PRIMARY KEY (`ReportID`)
) ENGINE = InnoDB;

