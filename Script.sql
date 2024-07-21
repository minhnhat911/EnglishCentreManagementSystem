USE EnglishCentreSysDB
GO
------------------AccountDL----------------------------------------------------------------------------------------------------------------------------------
--
CREATE PROC sp_GetAllAccounts
AS
BEGIN
    SELECT A.*, R.RoleName
    FROM Accounts A
    JOIN Roles R ON A.RoleID = R.RoleID
END
GO
--
CREATE PROC sp_AddAccount (
    @UserID VARCHAR(10),
    @Username NVARCHAR(50),
    @PasswordHash NVARCHAR(255),
    @RoleID INT,
    @IsActive BIT
)
AS
BEGIN
    INSERT INTO Accounts (UserID, Username, PasswordHash, RoleID, IsActive, LastLogin)
    VALUES (@UserID, @Username, @PasswordHash, @RoleID, @IsActive, NULL)
END
GO
--
CREATE PROC sp_UpdateAccount (
    @UserID VARCHAR(10),
    @Username NVARCHAR(50),
    @PasswordHash NVARCHAR(255),
    @RoleID INT,
    @IsActive BIT
)
AS
BEGIN
    UPDATE Accounts
    SET Username = @Username, PasswordHash = @PasswordHash,
        RoleID = @RoleID, IsActive = @IsActive
    WHERE UserID = @UserID
END
GO
----------------------------------
CREATE PROC sp_DeleteAccount (@UserID VARCHAR(10))
AS
BEGIN
    DELETE FROM Accounts WHERE UserID = @UserID
END
GO
----------------------------------
CREATE PROCEDURE sp_GetAccountByUserID
    @UserID VARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT a.AccountID, a.UserID, a.Username, a.PasswordHash, 
           a.RoleID, r.RoleName, a.IsActive, a.LastLogin
    FROM Accounts a
    JOIN Roles r ON a.RoleID = r.RoleID
    WHERE a.UserID = @UserID;
END;
GO

-----------------TeacherDL----------------------------------------------------------------------------------------------------------------------------------
CREATE PROC sp_GetTeacherViewModels
AS
BEGIN
    SELECT t.TeacherID, t.UserID, t.DegreeID,
           u.LastName, u.FirstName, u.Gender, u.DateOfBirth,
           d.DegreeName
    FROM Teachers t
    JOIN Users u ON t.UserID = u.UserID
    JOIN Degrees d ON t.DegreeID = d.DegreeID
END
-------------
GO
CREATE PROCEDURE sp_GetTeacherByUserID
    @UserID NVARCHAR(10)
AS
BEGIN
    SELECT * FROM Teachers WHERE UserID = @UserID
END

-------------
go

CREATE PROC sp_AddTeacher
    @TeacherID NVARCHAR(10),
    @UserID VARCHAR(10),
    @DegreeID VARCHAR(10)
AS
BEGIN
    INSERT INTO Teachers (TeacherID, UserID, DegreeID)
    VALUES (@TeacherID, @UserID, @DegreeID)
END
----------
GO
CREATE PROC sp_UpdateTeacher
    @UserID VARCHAR(10),
    @DegreeID NVARCHAR(50)
AS
BEGIN
    UPDATE Teachers
    SET DegreeID = @DegreeID
    WHERE UserID = @UserID
END
----------------
GO
CREATE PROC sp_DeleteTeacher
    @UserID VARCHAR(10)
AS
BEGIN
    DELETE FROM Teachers
    WHERE UserID = @UserID
END
------------
GO
CREATE PROC sp_GenerateTeacherID
AS
BEGIN
    DECLARE @LastID NVARCHAR(10)
    
    SELECT TOP 1 @LastID = TeacherID FROM Teachers ORDER BY TeacherID DESC
    
    IF @LastID IS NOT NULL
    BEGIN
        DECLARE @NewID NVARCHAR(10)
        SET @NewID = 'T' + RIGHT('000' + CAST(CAST(SUBSTRING(@LastID, 2, LEN(@LastID)) AS INT) + 1 AS NVARCHAR(3)), 3)
        SELECT @NewID AS TeacherID
    END
    ELSE
    BEGIN
        SELECT 'T001' AS TeacherID
    END
END
------------------------STUDENTDL----------------------------------------------------------------------------------------------------------
GO
CREATE PROCEDURE sp_GetStudentByUserID
    @UserID NVARCHAR(10)
AS
BEGIN
    SELECT * FROM Students WHERE UserID = @UserID
END
GO
------------------------PlacementTest----------------------------------------------------------------------------------------------------------
USE EnglishCentreSysDB
GO
CREATE PROCEDURE sp_AddPlacementTest
    @TestID NVARCHAR(10),
    @StudentID NVARCHAR(10),
    @TestDate DATE,
    @Score FLOAT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @LevelName NVARCHAR(50);
    SELECT @LevelName = LevelName 
    FROM ProficiencyLevels 
    WHERE @Score BETWEEN MinScore AND MaxScore;

    INSERT INTO PlacementTests (TestID, StudentID, TestDate, Score, LevelName)
    VALUES (@TestID, @StudentID, @TestDate, @Score, @LevelName);
END
--------------------------------------------
GO
CREATE PROCEDURE sp_UpdatePlacementTest
    @TestID NVARCHAR(10),
    @TestDate DATE,
    @Score FLOAT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @LevelName NVARCHAR(50);
    SELECT @LevelName = LevelName 
    FROM ProficiencyLevels 
    WHERE @Score BETWEEN MinScore AND MaxScore;

    UPDATE PlacementTests
    SET TestDate = @TestDate,
        Score = @Score,
        LevelName = @LevelName
    WHERE TestID = @TestID;
END
-------------------------------------------
GO
CREATE PROCEDURE sp_DeletePlacementTest
    @TestID NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM PlacementTests
    WHERE TestID = @TestID;
END
-------------------------------------------
GO

--------------------------------------
CREATE PROCEDURE sp_GetScoreFrequencyByYear
    @Year INT
AS
BEGIN
    SELECT 
        pt.Score,
        COUNT(*) AS Frequency
    FROM PlacementTests pt
    WHERE YEAR(pt.TestDate) = @Year
    GROUP BY pt.Score
    ORDER BY pt.Score
END
go
-------------------COURSEDL------------------------------------------------------------------------------------------
USE EnglishCentreSysDB
GO
-- Lấy tất cả khóa học
CREATE PROCEDURE sp_GetAllCourses
AS
BEGIN
    SELECT * FROM Courses
END
GO

-- Thêm khóa học
CREATE PROCEDURE sp_AddCourse
    @CourseID VARCHAR(10),
    @CourseName NVARCHAR(100),
    @Description NVARCHAR(255),
    @Duration INT,
    @Fee DECIMAL(10, 2)
AS
BEGIN
    INSERT INTO Courses (CourseID, CourseName, Description, Duration, Fee)
    VALUES (@CourseID, @CourseName, @Description, @Duration, @Fee)
END
GO

-- Cập nhật khóa học
CREATE PROCEDURE sp_UpdateCourse
    @CourseID VARCHAR(10),
    @CourseName NVARCHAR(100),
    @Description NVARCHAR(255),
    @Duration INT,
    @Fee DECIMAL(10, 2)
AS
BEGIN
    UPDATE Courses
    SET CourseName = @CourseName,
        Description = @Description,
        Duration = @Duration,
        Fee = @Fee
    WHERE CourseID = @CourseID
END
GO

-- Xóa khóa học
CREATE PROCEDURE sp_DeleteCourse
    @CourseID VARCHAR(10)
AS
BEGIN
    DELETE FROM Courses WHERE CourseID = @CourseID
END
GO
--------------------ProficiencyLevels-----------------------------------------------------------------------------------------------
USE EnglishCentreSysDB
GO
CREATE PROCEDURE sp_GetAllProficiencyLevels
AS
BEGIN
    SELECT LevelName
    FROM ProficiencyLevels
END
GO
--------------------CLASSES-------------------------------------------------------------------------------------------------------
USE EnglishCentreSysDB
GO
-- Thêm lớp học------------------------
CREATE PROCEDURE sp_AddClass
    @ClassID NVARCHAR(10),
    @CourseID NVARCHAR(10),
    @StartDate DATE,
    @EndDate DATE,
    @Room NVARCHAR(50),
    @MaxStudents INT
AS
BEGIN
    INSERT INTO Classes VALUES (@ClassID, @CourseID, @StartDate, @EndDate, @Room, @MaxStudents)
END

GO
-- Sửa lớp học---------------------------
CREATE PROCEDURE sp_UpdateClass
    @ClassID NVARCHAR(10),
    @CourseID NVARCHAR(10),
    @StartDate DATE,
    @EndDate DATE,
    @Room NVARCHAR(50),
    @MaxStudents INT
AS
BEGIN
    UPDATE Classes SET CourseID = @CourseID, StartDate = @StartDate,
        EndDate = @EndDate, Room = @Room, MaxStudents = @MaxStudents
    WHERE ClassID = @ClassID
END

GO

-- Xóa lớp học---------------------
CREATE PROCEDURE sp_DeleteClass
    @ClassID NVARCHAR(10)
AS
BEGIN
    DELETE FROM Schedules WHERE ClassID = @ClassID
    DELETE FROM ClassTeachers WHERE ClassID = @ClassID
    DELETE FROM ClassStudents WHERE ClassID = @ClassID
    DELETE FROM Classes WHERE ClassID = @ClassID
END
GO
----------------------------------
CREATE PROCEDURE sp_GetClassByID
    @ClassID NVARCHAR(10)
AS
BEGIN
    SELECT ClassID, CourseID, Room, StartDate, EndDate, MaxStudents
    FROM Classes
    WHERE ClassID = @ClassID
END

go
-- Lấy danh sách lớp học---------------------
CREATE PROCEDURE sp_GetAllClasses
AS
BEGIN
    SELECT c.*, cr.CourseName
    FROM Classes c
    JOIN Courses cr ON c.CourseID = cr.CourseID
END
GO

-- Thêm lịch học
CREATE PROCEDURE sp_AddSchedule
    @ScheduleID NVARCHAR(10),
    @ClassID NVARCHAR(10),
    @ClassDate DATE,
    @StartTime TIME,
    @EndTime TIME,
    @Room NVARCHAR(50)
AS
BEGIN
    INSERT INTO Schedules VALUES (@ScheduleID, @ClassID, @ClassDate, @StartTime, @EndTime, @Room)
END
GO
---- Cập nhật lịch học
CREATE PROCEDURE sp_UpdateSchedule
    @ScheduleID NVARCHAR(10),
    @ClassDate DATE,
    @StartTime TIME,
    @EndTime TIME,
    @Room NVARCHAR(50)
AS
BEGIN
    UPDATE Schedules SET ClassDate = @ClassDate, StartTime = @StartTime,
        EndTime = @EndTime, Room = @Room
    WHERE ScheduleID = @ScheduleID
END
GO
----
-- Xóa lịch học
CREATE PROCEDURE sp_DeleteSchedule
    @ScheduleID NVARCHAR(10)
AS
BEGIN
    DELETE FROM Schedules WHERE ScheduleID = @ScheduleID
END
go
------
CREATE PROCEDURE sp_GetAllSchedules
AS
BEGIN
    SELECT s.ScheduleID, s.ClassID, s.ClassDate, s.StartTime, s.EndTime, s.Room, 
           c.ClassID AS ClassName
    FROM Schedules s
    JOIN Classes c ON s.ClassID = c.ClassID;
END
go
-------------------------------FEES------------------------------------------------------------------
CREATE PROCEDURE sp_GetAllStudentTuitionFees
AS
BEGIN
    SELECT 
        stf.TuitionID,
        stf.StudentID,
        u.LastName + ' ' + u.FirstName AS StudentName,
        stf.ClassID,
        c.CourseName,
        stf.Fee,
        stf.Status,
        stf.CreatedAt
    FROM StudentTuitionFees stf
    JOIN Students s ON stf.StudentID = s.StudentID
    JOIN Users u ON s.UserID = u.UserID
    JOIN Classes cls ON stf.ClassID = cls.ClassID
    JOIN Courses c ON cls.CourseID = c.CourseID
    ORDER BY stf.CreatedAt DESC
END


