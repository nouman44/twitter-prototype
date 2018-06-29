Create Trigger OnUserFollowed --if a user is followed by some other user, 
On UserFollowers			--then followers table will be updated and on that update this update trigger 
After Insert				--will be called that will update the UserStats Table of this User
As 
Begin

	declare @UserName varchar(15);

	select @UserName = UserName FROM inserted
	
	Update UserStats
		Set [UserFollowers]=[UserFollowers]+1
		Where UserName=@UserName

End
Go

--Trigger on UserFollowings 
Create Trigger OnUserFollowing --if a user is following an other user, 
On UserFollowing			--then following table will be updated and on that update this update trigger 
After Insert				--will be called that will update the UserStats Table of this User
As 
Begin
	
	declare @UserName varchar(15);

	select @UserName = UserName FROM inserted
	
	Update UserStats
		Set [UserFollowing]=[UserFollowing]+1
		Where UserName=@UserName

End
Go


Create Trigger OnUserUnfollowing --if a user unfollows another user
On UserFollowing
After Delete
As 
Begin
	
	declare @UserName varchar(15);

	select @UserName = UserName FROM deleted
	
	Update UserStats
		Set [UserFollowing]=[UserFollowing]-1
		Where UserName=@UserName

End
Go

Create Trigger OnUserUnFollowed --if a user is unfollowed by another user
On UserFollowers
After Delete
As 
Begin
	
	declare @UserName varchar(15);

	select @Username = UserName FROM deleted
	
	Update UserStats
		Set [UserFollowers]=[UserFollowers]-1
		Where UserName=@UserName

End
Go

Create Trigger OnTweet --if a tweet is posted by a user
On Tweets
After Insert
As
Begin
	declare @UserName varchar(15);

	select @UserName = UserName From inserted

	Update UserStats
		Set Tweets = Tweets + 1
		Where UserName = @UserName
End 
Go

Create Trigger OnTweetDelete
On Tweets
After Delete
As
Begin
	declare @UserName varchar(15);

	select @UserName = UserName From deleted

	Update UserStats
		Set Tweets = Tweets - 1
		Where UserName = @UserName
End
Go

