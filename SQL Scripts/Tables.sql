create table UserDetails(

	UserName varchar(15) primary key not null,
	FirstName varchar(15) not null,
	LastName varchar(15) not null,
	Passcode varchar(10) not null,
	Bdate date,
	Email varchar(50) not null,
	City varchar(40),
	Phone varchar(13),
	Country varchar(40),
	Gender varchar(1)

);

alter table UserDetails add Privacy varchar(7)

create table Users(

	Username varchar(15) primary key not null,
	Passcode varchar(10) not null
	foreign key (UserName) references UserDetails(UserName) ON DELETE NO ACTION

);


create table Tweets(

	TweetID int primary key not null,
	UserName varchar(15) not null,
	Tweet varchar(140) not null,
	TweetDate Date not null,
	TweetTime Time not null

	foreign key (UserName) references Users(UserName) ON DELETE NO ACTION
);

create table Retweets
(
	RetweetID int primary key not null,
	OriginalTweetID int not null

	foreign key (RetweetID) references Tweets(TweetID) ON DELETE NO ACTION,
	foreign key (OriginalTweetID) references Tweets(TweetID) ON DELETE NO ACTION
);

create table HashTags(

	UserName varchar(15) not null,
	TweetID int not null,
	Hashtag varchar(40) not null

	foreign key (UserName) references Users(UserName) ON DELETE NO ACTION,
	foreign key (TweetID) references Tweets(TweetID) ON DELETE NO ACTION
);

create table Blocked(

	BlockedName varchar(15) not null,
	BlockerName varchar(15) not null,

	foreign key (BlockedName) references Users(UserName) ON DELETE NO ACTION,
	foreign key (BlockerName) references Users(UserName) ON DELETE NO ACTION,
	primary key(BlockedName,BlockerName)

);

drop table Blocked

create table UserFollowing(

	UserName varchar (15) not null,
	FUserName varchar (15) not null

	foreign key (UserName) references Users(UserName) ON DELETE NO ACTION,
	foreign key (FUserName) references Users(UserName) ON DELETE NO ACTION,
	primary key(UserName,FUsername)

);

create table UserFollowers(
	
	UserName varchar (15) not null,
	FUserName varchar (15) not null

	foreign key (UserName) references Users(UserName) ON DELETE NO ACTION,
	foreign key (FUserName) references Users(UserName) ON DELETE NO ACTION,
	primary key(UserName,FUsername)

);

create table UserStats(
	
	UserName varchar(15) primary key not null,
	UserFollowers int,
	UserFollowing int,
	Tweets int
	
);

create table MessageDetails(
	MessageID int primary key not null,
	SenderName varchar(15) not null,
	ReceiverName varchar(15) not null,
	Body varchar(140) not null,
	MessageTime time not null

	foreign key (SenderName) references UserDetails(UserName) ON DELETE NO ACTION,
	foreign key (ReceiverName) references UserDetails(UserName) ON DELETE NO ACTION,
);

Create View TrendingTweets
As
Select Top 10 Hashtag,COUNT(*) HashCount
From HashTags
Group By Hashtag
Order By COUNT(*) desc
Go

Select * From UserDetails

Select * From Users

Select * From UserFollowers

Select * From UserFollowing

Select * From UserStats

Select * From Blocked

Select * From Tweets

Select * From HashTags

Select * From MessageDetails

Select * From Retweets













