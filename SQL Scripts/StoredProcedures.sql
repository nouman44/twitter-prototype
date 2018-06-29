
--SignUp
Create Procedure SignUp
@UserName varchar(15),
@FirstName varchar(15),
@LastName varchar(15),
@Passcode varchar(10),
@Bdate date,
@Email varchar(50),
@City varchar(40),
@Phone varchar(13),
@Country varchar(40),
@Gender varchar(1),
@Flag int Output
As
Begin
	If Exists(
		Select *
		From UserDetails
		Where @UserName = UserName
	)
		Begin
			Set @Flag = 0;
		End
	Else
		Begin
			Insert into UserDetails values(@UserName,@FirstName,@LastName,@Passcode,@Bdate,@Email,@City,@Phone,@Country,@Gender,'Public');
			Insert into Users values(@UserName,@Passcode);
			Insert into UserStats values(@UserName,0,0,0);
			Set @Flag = 1;
		End
End
Go

drop procedure SignUp

--User Login
Create Procedure UserLogin
@UserName varchar(15),
@Passcode varchar(10),
@Flag1 int Output,
@Flag2 int Output
As 
Begin
	If Exists(
		Select *
		From Users
		Where @UserName = Username)
		Begin
			Set @Flag1 = 1;
			If Exists(
			Select *
			From Users
			Where @UserName = Username AND @Passcode = Passcode)
				Begin
					Set @Flag2 = 1;
				End
			Else
				Begin
					Set @Flag2 = 0;
				End
		End
	Else
		Begin
		Set @Flag1 = 0;
		Set @Flag2 =0;
		End
End
Go

--Create Tweet
Create Procedure PostTweet
@UserName varchar(15),
@Tweet varchar(140),
@TweetDate Date,
@TweetTime Time,
@TweetID int Output
As
Begin

	Declare @count int;

	Set @count = (Select top 1 TweetID
					From Tweets
					Order By TweetID desc);

	If not exists(
		select *
		from Tweets)
	Begin
		Set @count = 0;
	End

	Set @count = @count + 1;

	Insert into Tweets values(@count,@UserName,@Tweet,@TweetDate,@TweetTime)

	Set @TweetID = @count;
	
End
Go


--Update User Details
Create Procedure UpdateUserDetails
@UserName varchar(15),
@FirstName varchar(15),
@LastName varchar(15),
@Bdate date,
@City varchar(40),
@Phone varchar(13),
@Country varchar(40),
@Gender varchar(1),
@Privacy varchar(7)
As 
Begin

	Update UserDetails
	Set FirstName=@FirstName,LastName=@LastName,Bdate=@Bdate,City=@City,Phone=@Phone,Country=@Country,Gender=@Gender,Privacy=@Privacy
	Where UserName = @UserName

End
Go
drop procedure UpdateUserDetails

--Display User Details
Create Procedure GetUserDetails
@UserName varchar(15),
@FirstName varchar(15) Output,
@LastName varchar(15) Output,
@Bdate date Output,
@Email varchar(50) Output,
@City varchar(40) Output,
@Phone varchar(13) Output,
@Country varchar(40) Output,
@Gender varchar(1) Output,
@Privacy varchar(7) Output
As
Begin

	Select @FirstName=FirstName,@LastName=LastName,@Bdate=Bdate,@Email=Email,@City=City,@Phone=Phone,@Country=Country,@Gender=Gender,@Privacy=Privacy
	From UserDetails
	Where UserName = @UserName

End
Go

--Get FirstName and LastName of User
Create Procedure GetName
@UserName varchar(15),
@FirstName varchar(15) Output,
@LastName varchar(15)Output
As
Begin
	Select @FirstName=FirstName,@LastName=LastName
	From UserDetails
	Where UserName=@UserName
End
Go

--Hashtags    inserts a Hashtag into the Hashtag table by a User in the specific tweet of the user.
Create Procedure CreateHashtag 
@UserName varchar(15),
@TweetID int,
@Hashtag varchar(40)
As
Begin

	insert into HashTags values(@UserName,@TweetID,@Hashtag); 

End
Go

--Block User
Create Procedure BlockUser
@BlockedName varchar(15),
@BlockerName varchar(15)
As
Begin

	Insert into Blocked values(@BlockedName,@BlockerName);

	Delete From UserFollowing
	Where (FUserName = @BlockedName AND Username = @BlockerName) OR (FUserName = @BlockerName AND Username = @BlockedName)

	Delete From UserFollowers
	Where (FUserName = @BlockedName AND Username = @BlockerName) OR (FUserName = @BlockerName AND Username = @BlockedName)

End 
Go

Create Procedure UnblockUser
@BlockedName varchar(15),
@BlockerName varchar(15)
As
Begin
	Delete from Blocked
	Where BlockedName = @BlockedName AND BlockerName=@BlockerName
End
Go

Create Procedure GetBlockedUsers
@UserName varchar(15)
As
Begin
	Select *
	From Blocked 
	Where  BlockerName=@UserName

End
Go

Create Procedure CheckIsBlocked
@BlockedName varchar(15),
@BlockerName varchar(15),
@Flag int Output
As
Begin
	if exists (
		Select *
		From Blocked
		Where (BlockedName=@BlockedName AND BlockerName=@BlockerName) OR (BlockedName=@BlockerName AND BlockerName=@BlockedName)
	)
	Begin 
		Set @Flag =1;
	End
	Else
	Begin
		Set @Flag=0;
	End

End
Go

-- Change Passcode
CREATE PROCEDURE ChangePasscode
@UserName VARCHAR(15),
@OldPasscode VARCHAR(10),
@NewPasscode VARCHAR(10)
AS
BEGIN
	-- Sets new passcode in table if Username matches and Old (Current) Password also matches
	UPDATE Users SET Passcode = @NewPasscode WHERE Username = @UserName AND Passcode = @OldPasscode
END
GO

-- Displays User's Stats
CREATE PROCEDURE DisplayStats
@UserName VARCHAR(15),
@UsersFollowers INT OUTPUT,
@UsersFollowing INT OUTPUT,
@Tweets INT OUTPUT
AS
BEGIN
	IF EXISTS (SELECT * FROM Users WHERE Username = @UserName)
	BEGIN
		-- If User exists set the stats of the user in output variables
		SELECT @UsersFollowing = UserFollowing, @UsersFollowers = UserFollowers, @Tweets = Tweets
		FROM UserStats
		WHERE Username = @UserName
	END
	ELSE
	BEGIN
		SET @UsersFollowers = -1;	--
		SET @UsersFollowing = -1;	-- If User doesn't exist, -1 is returned
		SET @Tweets = -1;			--
	END
END
GO

--Allow user to follow another user
Create Procedure FollowUser
@UserName varchar (15),
@FUserName varchar (15)
As
Begin
	Insert into UserFollowers values(@FUserName,@UserName);
	Insert into UserFollowing values(@UserName,@FUserName);
End
Go

--Allow user to follow another user
Create Procedure UnfollowUser
@UserName varchar (15),
@FUserName varchar (15)
As
Begin
	Delete From UserFollowers
	Where FUserName = @UserName AND UserName = @FUserName

	Delete From UserFollowing
	Where FUserName = @FUserName AND UserName = @UserName
End
Go

--Allow user to follow another user
Create Procedure SearchUsers
@UserString varchar(15),
@Flag int Output
As
Begin
	If Exists(
		Select * From UserDetails
		Where UserName like '%'+@UserString+'%')
		Begin
			Set @Flag = 1;

			Select UserName,FirstName,LastName From UserDetails
			Where UserName like '%'+@UserString+'%'
		End
	Else
		Begin
			Set @Flag = 0;
		End
End 
Go

--Allow user to search for hashtag(s)
Create Procedure SearchHashtags
@HashString varchar(40),
@Flag int Output
As
Begin
	If Exists(
		Select * From Tweets
		Where Tweet like '%'+@HashString+'%')
		Begin
			Set @Flag = 1;

			Select * From Tweets
			Where Tweet like '%'+@HashString+'%'
		End
	Else
		Begin
			Set @Flag = 0;
		End
End 
Go

Create Procedure ShowUserTweets
@UserName varchar(15)
As
Begin
	Select *
	From Tweets
	Where @UserName=UserName OR Tweet like '%'+@UserName+'%'
	Order By TweetID desc
End
Go

drop procedure ShowFollowedTweets


Create Procedure ShowFollowedTweets
@UserName varchar(15)
As
Begin
	Select T.TweetID,T.UserName,T.Tweet,T.TweetDate,T.TweetTime
	From UserFollowing UF join Tweets T on UF.FUserName=T.UserName
	Where @UserName = UF.UserName
	Order By TweetID desc
End
Go

Create Procedure Retweet
@RUserName varchar(15),
@RTweetDate Date,
@RTweetTime Time,
@OriginalTweetID int
As
Begin

	Declare @count int;

	Set @count = (Select top 1 TweetID
					From Tweets
					Order By TweetID desc);

	If not exists(
		select *
		from Tweets)
	Begin
		Set @count = 0;
	End

	Set @count = @count + 1;

	Insert into Tweets values(@count,@RUserName,'',@RTweetDate,@RTweetTime);
	Insert into Retweets values(@count,@OriginalTweetID);
	
End
Go

Create Procedure CheckRetweet
@TweetID int,
@OriginalTweetID int Output
As 
Begin
	If exists(
		Select *
		From Retweets
		Where @TweetID = RetweetID
	)
		Begin
			Select @OriginalTweetID = OriginalTweetID
			From Retweets
			Where RetweetID=@TweetID
		End
	Else
		Begin
			Set @OriginalTweetID = 0;
		End
End
Go

Create Procedure GetTweet
@TweetID int,
@Username varchar(15) Output,
@Tweet varchar(140) Output,
@TweetDate Date Output,
@TweetTime Time Output
As
Begin
	Select @Username=UserName,@Tweet=Tweet,@TweetDate=TweetDate,@TweetTime=TweetTime
	From Tweets
	Where @TweetID=TweetID
End
Go

Create Procedure DeleteTweet
@TweetID int
As
Begin

	If exists(
		Select RetweetID
		From Retweets
		Where OriginalTweetID = @TweetID
	)
	Begin

		Select RetweetID into #TweetsToDel
		From Retweets
		Where OriginalTweetID = @TweetID

		Delete From Retweets Where OriginalTweetID=@TweetID

		Delete From Tweets 
		Where TweetID in (
		Select RetweetID from #TweetsToDel)

		drop table #TweetsToDel
	End
	Else
	Begin
		Delete From Retweets Where RetweetID=@TweetID
	End

	Delete From HashTags
	Where TweetID=@TweetID

	Delete From Tweets
	Where TweetID=@TweetID

End
Go

drop procedure DeleteTweet

Execute DeleteTweet
@TweetID=11
Go


Create Procedure GetUserFollowers
@UserName varchar(15)
As
Begin

	Select FUserName
	From UserFollowers
	Where UserName = @UserName

End 
Go

Create Procedure GetUserFollowing
@UserName varchar(15)
As
Begin

	Select FUserName
	From UserFollowing
	Where UserName = @UserName

End 
Go

Create Procedure CheckIsFollower
@UserName varchar(15),
@FUserName varchar(15),
@Flag int Output
As
Begin
	if exists(
		Select *
		From UserFollowers
		Where UserName=@UserName AND FUserName=@FUserName
	)
	Begin
		Set @Flag = 1;
	End
	Else
	Begin
		Set @Flag = 0;
	End
End
Go

drop procedure CheckIsFollower

--Messaging         Procedures inserts a new message in MessageDetails table. For Example, when a user sends a message to another user.
Create Procedure CreateMessage
@SenderName varchar(15),
@ReceiverName varchar(15),
@Body varchar(140),
@MessageTime time
As
Begin

	Declare @count int;

	Select @count = COUNT(MessageID)
	From MessageDetails

	insert into MessageDetails values(@count+1,@SenderName,@ReceiverName,@Body,@MessageTime);
End

Create Procedure GetConvos
@UserName varchar(15)
As
Begin
	Select SenderName,ReceiverName
	From MessageDetails
	Where SenderName = @UserName OR ReceiverName = @UserName
	Group By SenderName,ReceiverName

End
Go

Create Procedure GetMessages
@UserName varchar(15),
@SecUserName varchar(15)
As
Begin
	Select *
	From MessageDetails
	Where (SenderName=@UserName AND ReceiverName=@SecUserName) OR (SenderName=@SecUserName AND ReceiverName=@UserName)
End
Go

Create Procedure DeleteUser
@Username varchar(15)
As
Begin

	Delete From MessageDetails
	Where SenderName=@Username OR ReceiverName=@Username

	Delete From Blocked
	Where BlockedName=@Username OR BlockerName=@Username

	--Loop to delete User Tweets
	DECLARE @TweetID INT = 0

	-- Iterate over all Tweets
	WHILE (1 = 1) 
	BEGIN  

	  -- Get next TweetID
	  SELECT TOP 1 @TweetID = TweetID
	  FROM Tweets
	  WHERE (TweetID > @TweetID)  AND (UserName = @Username)
	  ORDER BY TweetID

	  -- Exit loop if no more customers
	  IF @@ROWCOUNT = 0 BREAK;

	  -- call your sproc
	  EXEC DeleteTweet @TweetID

	END

	Delete From UserFollowers
	Where (FUserName=@Username) OR UserName=@Username

	Delete From UserFollowing
	Where (FUserName=@Username) OR UserName=@Username

	Delete from UserStats
	Where UserName=@Username

	Delete From Users
	Where UserName=@Username

	Delete From UserDetails
	Where Username=@Username

End
Go















