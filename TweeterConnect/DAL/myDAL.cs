using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace TweeterConnect.DAL
{
    public class myDAL
    {
        private static readonly string connString =
        System.Configuration.ConfigurationManager.ConnectionStrings["tweeter"].ConnectionString;

        public int VerifyLogin(string UserName, string Password)
        {
            int flagUser = 0, flagPass = 0, flagAns = 0;
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("UserLogin", con); //name of your SQL procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 15); //input of SQL stored procedure
                cmd.Parameters.Add("@Passcode", SqlDbType.VarChar, 10);

                // set SQL procedure parameter values
                cmd.Parameters["@UserName"].Value = UserName;
                cmd.Parameters["@Passcode"].Value = Password;

                SqlParameter output = new SqlParameter("@Flag1", SqlDbType.Int);
                SqlParameter output1 = new SqlParameter("@Flag2", SqlDbType.Int);
                output.Direction = ParameterDirection.Output;
                output1.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output);
                cmd.Parameters.Add(output1);

                cmd.ExecuteNonQuery(); //executre the cmd query

                flagUser = Convert.ToInt32(output.Value);
                flagPass = Convert.ToInt32(output1.Value);

                if (flagUser == 1 && flagPass == 0)
                {
                    flagAns = 1;
                }
                else if (flagUser == 0 && flagPass == 0)
                {
                    flagAns = 0;
                }
                else if (flagUser == 1 && flagPass == 1)
                {
                    flagAns = 2;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }

            return flagAns;
        }

        public int DoSignUp(string userName, string password, string firstName, string lastName, DateTime bDate, string email, string city, string phone, string country, string gender)
        {
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;
            int flag = 0;

            try
            {
                cmd = new SqlCommand("SignUp", con); //name of your SQL procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 15); //input of SQL stored procedure
                cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 15);
                cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 15); //input of SQL stored procedure
                cmd.Parameters.Add("@Passcode", SqlDbType.VarChar, 10);
                cmd.Parameters.Add("@Bdate", SqlDbType.Date); //input of SQL stored procedure
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50);
                cmd.Parameters.Add("@City", SqlDbType.VarChar, 40); //input of SQL stored procedure
                cmd.Parameters.Add("@Phone", SqlDbType.VarChar, 13);
                cmd.Parameters.Add("@Country", SqlDbType.VarChar, 40);
                cmd.Parameters.Add("@Gender", SqlDbType.VarChar, 1);

                // set SQL procedure parameter values
                cmd.Parameters["@UserName"].Value = userName;
                cmd.Parameters["@FirstName"].Value = firstName;
                cmd.Parameters["@LastName"].Value = lastName;
                cmd.Parameters["@Passcode"].Value = password;
                cmd.Parameters["@Bdate"].Value = bDate;
                cmd.Parameters["@Email"].Value = email;
                cmd.Parameters["@City"].Value = city;
                cmd.Parameters["@Phone"].Value = phone;
                cmd.Parameters["@Country"].Value = country;
                cmd.Parameters["@Gender"].Value = gender;

                SqlParameter output = new SqlParameter("@Flag", SqlDbType.Int);
                output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output);

                cmd.ExecuteNonQuery(); //executre the cmd query

                flag = Convert.ToInt32(output.Value);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }

            return flag;
        }

        public void GetName(String username,ref String firstName,ref String lastName)
        {
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("GetName", con); //name of your SQL procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 15); //input of SQL stored procedure
                cmd.Parameters["@UserName"].Value = username;

                SqlParameter fName = new SqlParameter("@FirstName", SqlDbType.VarChar, 15);
                SqlParameter lName = new SqlParameter("@LastName", SqlDbType.VarChar, 15);
                fName.Direction = ParameterDirection.Output;
                lName.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(fName);
                cmd.Parameters.Add(lName);

                cmd.ExecuteNonQuery();

                firstName = Convert.ToString(fName.Value);
                lastName = Convert.ToString(lName.Value);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        public void GetUserStats(String username,ref int noTweets,ref int noFollowing,ref int noFollowers)
        {
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("DisplayStats", con); //name of your SQL procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 15); //input of SQL stored procedure
                cmd.Parameters["@UserName"].Value = username;

                SqlParameter userFollowers = new SqlParameter("@UsersFollowers", SqlDbType.Int);
                SqlParameter userFollowing = new SqlParameter("@UsersFollowing", SqlDbType.Int);
                SqlParameter Tweets = new SqlParameter("@Tweets", SqlDbType.Int);
                userFollowers.Direction = ParameterDirection.Output;
                userFollowing.Direction = ParameterDirection.Output;
                Tweets.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(userFollowers);
                cmd.Parameters.Add(userFollowing);
                cmd.Parameters.Add(Tweets);

                cmd.ExecuteNonQuery();

                noFollowers = Convert.ToInt32(userFollowers.Value);
                noFollowing = Convert.ToInt32(userFollowing.Value);
                noTweets = Convert.ToInt32(Tweets.Value);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        private void StoreHashTag(String username,int tweetID,string hashTag)
        {
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("CreateHashtag", con); //name of your SQL procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 15); //input of SQL stored procedure
                cmd.Parameters["@UserName"].Value = username;

                cmd.Parameters.Add("@TweetID", SqlDbType.Int); //input of SQL stored procedure
                cmd.Parameters["@TweetID"].Value = tweetID;

                cmd.Parameters.Add("@Hashtag", SqlDbType.VarChar, 40); //input of SQL stored procedure
                cmd.Parameters["@Hashtag"].Value = hashTag;

                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        public void PostTweet(String username, String tweet, DateTime date, DateTime time)
        {
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("PostTweet", con); //name of your SQL procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 15); //input of SQL stored procedure
                cmd.Parameters["@UserName"].Value = username;

                cmd.Parameters.Add("@Tweet", SqlDbType.VarChar, 140); //input of SQL stored procedure
                cmd.Parameters["@Tweet"].Value = tweet;

                cmd.Parameters.Add("@TweetDate", SqlDbType.Date); //input of SQL stored procedure
                cmd.Parameters["@TweetDate"].Value = date.Date;

                cmd.Parameters.Add("@TweetTime", SqlDbType.Time); //input of SQL stored procedure
                cmd.Parameters["@TweetTime"].Value = time.TimeOfDay;

                SqlParameter OTweetID = new SqlParameter("@TweetID", SqlDbType.Int);
                OTweetID.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(OTweetID);

                cmd.ExecuteNonQuery();

                int tweetID = Convert.ToInt32(OTweetID.Value);
                bool found = false;
                char[] arr = new char[40];
                int j = 0;
                String hashTag;

                for (int i = 0; i < tweet.Length; i++)
                {
                    if (tweet[i] == '#')
                        found = true;
                    else if (tweet[i] == ' ' && found)
                    {
                        found = false;
                        hashTag = new string(arr, 0, j);
                        StoreHashTag(username, tweetID, hashTag);
                        j = 0;
                    }

                    if (found)
                    {
                        arr[j] = tweet[i];
                        j++;
                    }
                }

                if (found) //handle hashtag at end of post
                {
                    hashTag = new string(arr, 0, j);
                    StoreHashTag(username, tweetID, hashTag);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        public void ShowUserTweets(String username,ref DataTable result)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("ShowUserTweets", con); //name of your SQL procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 15); //input of SQL stored procedure
                cmd.Parameters["@UserName"].Value = username;

                cmd.ExecuteNonQuery();

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    da.Fill(ds); //get results

                result = ds.Tables[0]; //fill the results in ref input table 
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        public void ShowFollowedTweets(String username, ref DataTable result)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("ShowFollowedTweets", con); //name of your SQL procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 15); //input of SQL stored procedure
                cmd.Parameters["@UserName"].Value = username;

                cmd.ExecuteNonQuery();

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    da.Fill(ds); //get results

                result = ds.Tables[0]; //fill the results in ref input table 
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        public void ShowTrendingTweets(ref DataTable result)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;

            try
            {
                string sqlQuery = "Select * from TrendingTweets";
                cmd = new SqlCommand(sqlQuery, con);
                cmd.CommandType = CommandType.Text;

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    da.Fill(ds); //get results

                result = ds.Tables[0]; //fill the results in ref input table 
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        public void Retweet(String RUsername, DateTime RDate, DateTime RTime,int OTweetID)
        {
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("Retweet", con); //name of your SQL procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@RUserName", SqlDbType.VarChar, 15); //input of SQL stored procedure
                cmd.Parameters["@RUserName"].Value = RUsername;

                cmd.Parameters.Add("@RTweetDate", SqlDbType.Date); //input of SQL stored procedure
                cmd.Parameters["@RTweetDate"].Value = RDate.Date;

                cmd.Parameters.Add("@RTweetTime", SqlDbType.Time); //input of SQL stored procedure
                cmd.Parameters["@RTweetTime"].Value = RTime.TimeOfDay;

                cmd.Parameters.Add("@OriginalTweetID", SqlDbType.Int); //input of SQL stored procedure
                cmd.Parameters["@OriginalTweetID"].Value = OTweetID;

                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        public int CheckRetweet(int tweetID)
        {
            int oTweetID = 0;

            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("CheckRetweet", con); //name of your SQL procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@TweetID", SqlDbType.Int); //input of SQL stored procedure

                // set SQL procedure parameter values
                cmd.Parameters["@TweetID"].Value = tweetID;

                SqlParameter output = new SqlParameter("@OriginalTweetID", SqlDbType.Int);
                output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output);

                cmd.ExecuteNonQuery(); //executre the cmd query

                oTweetID = Convert.ToInt32(output.Value);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }

            return oTweetID;
        }

        public void GetTweet(int tweetID, ref String username, ref String tweet, ref DateTime date, ref DateTime time)
        {
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("GetTweet", con); //name of your SQL procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@TweetID", SqlDbType.Int); //input of SQL stored procedure
                cmd.Parameters["@TweetID"].Value = tweetID;

                SqlParameter output = new SqlParameter("@Username", SqlDbType.VarChar, 15);
                output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output);

                SqlParameter output1 = new SqlParameter("@Tweet", SqlDbType.VarChar, 140);
                output1.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output1);

                SqlParameter output2 = new SqlParameter("@TweetDate", SqlDbType.DateTime);
                output2.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output2);

                SqlParameter output3 = new SqlParameter("@TweetTime", SqlDbType.DateTime);
                output3.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output3);

                cmd.ExecuteNonQuery();

                username = Convert.ToString(output.Value);
                tweet = Convert.ToString(output1.Value);
                date = Convert.ToDateTime(output2.Value);
                time = Convert.ToDateTime(output3.Value);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        public void DeleteTweet(int tweetID)
        {
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("DeleteTweet", con); //name of your SQL procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@TweetID", SqlDbType.Int); //input of SQL stored procedure
                cmd.Parameters["@TweetID"].Value = tweetID;

                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        public void GetUserFollowers(String username,ref DataTable result)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd; 

            try
            {
                cmd = new SqlCommand("GetUserFollowers", con); //name of your SQL procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 15); //input of SQL stored procedure
                cmd.Parameters["@UserName"].Value = username;

                cmd.ExecuteNonQuery();

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    da.Fill(ds); //get results

                result = ds.Tables[0]; //fill the results in ref input table
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        public void GetUserFollowing(String username, ref DataTable result)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd; 

            try
            {
                cmd = new SqlCommand("GetUserFollowing", con); //name of your SQL procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 15); //input of SQL stored procedure
                cmd.Parameters["@UserName"].Value = username;

                cmd.ExecuteNonQuery();

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    da.Fill(ds); //get results

                result = ds.Tables[0]; //fill the results in ref input table
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        public int CheckIsFollower(String username,String fUsername)
        {
            int flag = 0;

            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("CheckIsFollower", con); //name of your SQL procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 15); //input of SQL stored procedure
                cmd.Parameters["@UserName"].Value = username;

                cmd.Parameters.Add("@FUserName", SqlDbType.VarChar, 15); //input of SQL stored procedure
                cmd.Parameters["@FUserName"].Value = fUsername;

                SqlParameter output = new SqlParameter("@Flag", SqlDbType.Int);
                output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output);

                cmd.ExecuteNonQuery(); //executre the cmd query

                flag = Convert.ToInt32(output.Value);

            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }

            return flag;
        }

        public void FollowUser(String username, String fUsername)
        {
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("FollowUser", con); //name of your SQL procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 15); //input of SQL stored procedure
                cmd.Parameters["@UserName"].Value = username;

                cmd.Parameters.Add("@FUserName", SqlDbType.VarChar, 15); //input of SQL stored procedure
                cmd.Parameters["@FUserName"].Value = fUsername;

                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        public void UnfollowUser(String username, String fUsername)
        {
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("UnfollowUser", con); //name of your SQL procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 15); //input of SQL stored procedure
                cmd.Parameters["@UserName"].Value = username;

                cmd.Parameters.Add("@FUserName", SqlDbType.VarChar, 15); //input of SQL stored procedure
                cmd.Parameters["@FUserName"].Value = fUsername;

                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        public void GetUserDetails(String username, ref String firstName, ref String lastName, ref DateTime birthDate, ref String email, ref String city, ref String phone, ref String country, ref String gender, ref String privacy)
        {
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("GetUserDetails", con); //name of your SQL procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 15); //input of SQL stored procedure
                cmd.Parameters["@UserName"].Value = username;

                SqlParameter oFName = new SqlParameter("@FirstName", SqlDbType.VarChar, 15);
                oFName.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(oFName);

                SqlParameter oLName = new SqlParameter("@LastName", SqlDbType.VarChar, 15);
                oLName.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(oLName);

                SqlParameter oBDate = new SqlParameter("@Bdate", SqlDbType.DateTime);
                oBDate.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(oBDate);

                SqlParameter oEmail = new SqlParameter("@Email", SqlDbType.VarChar, 50);
                oEmail.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(oEmail);

                SqlParameter oCity = new SqlParameter("@City", SqlDbType.VarChar, 40);
                oCity.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(oCity);

                SqlParameter oPhone = new SqlParameter("@Phone", SqlDbType.VarChar, 13);
                oPhone.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(oPhone);

                SqlParameter oCountry = new SqlParameter("@Country", SqlDbType.VarChar, 40);
                oCountry.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(oCountry);

                SqlParameter oGender = new SqlParameter("@Gender", SqlDbType.VarChar, 1);
                oGender.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(oGender);

                SqlParameter oPrivacy = new SqlParameter("@Privacy", SqlDbType.VarChar, 7);
                oPrivacy.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(oPrivacy);

                cmd.ExecuteNonQuery();

                firstName = Convert.ToString(oFName.Value);
                lastName = Convert.ToString(oLName.Value);
                birthDate = Convert.ToDateTime(oBDate.Value);
                email = Convert.ToString(oEmail.Value);
                city = Convert.ToString(oCity.Value);
                phone = Convert.ToString(oPhone.Value);
                country = Convert.ToString(oCountry.Value);
                gender = Convert.ToString(oGender.Value);
                privacy = Convert.ToString(oPrivacy.Value);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        public void UpdateUserDetails(String username, String firstName, String lastName, DateTime birthDate, String city,String phone, String country, String gender, String privacy)
        {
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("UpdateUserDetails", con); //name of your SQL procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 15); //input of SQL stored procedure
                cmd.Parameters["@UserName"].Value = username;

                cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 15); //input of SQL stored procedure
                cmd.Parameters["@FirstName"].Value = firstName;

                cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 15); //input of SQL stored procedure
                cmd.Parameters["@LastName"].Value = lastName;

                cmd.Parameters.Add("@Bdate", SqlDbType.DateTime); //input of SQL stored procedure
                cmd.Parameters["@Bdate"].Value = birthDate.Date;

                cmd.Parameters.Add("@City", SqlDbType.VarChar, 40); //input of SQL stored procedure
                cmd.Parameters["@City"].Value = city;

                cmd.Parameters.Add("@Phone", SqlDbType.VarChar, 13); //input of SQL stored procedure
                cmd.Parameters["@Phone"].Value = phone;

                cmd.Parameters.Add("@Country", SqlDbType.VarChar, 40); //input of SQL stored procedure
                cmd.Parameters["@Country"].Value = country;

                cmd.Parameters.Add("@Gender", SqlDbType.VarChar, 1); //input of SQL stored procedure
                cmd.Parameters["@Gender"].Value = gender;

                cmd.Parameters.Add("@Privacy", SqlDbType.VarChar, 15); //input of SQL stored procedure
                cmd.Parameters["@Privacy"].Value = privacy;

                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        public void BlockUser(String blockedName, String blockerName)
        {
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("BlockUser", con); //name of your SQL procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@BlockedName", SqlDbType.VarChar, 15); //input of SQL stored procedure
                cmd.Parameters["@BlockedName"].Value = blockedName;

                cmd.Parameters.Add("@BlockerName", SqlDbType.VarChar, 15); //input of SQL stored procedure
                cmd.Parameters["@BlockerName"].Value = blockerName;

                cmd.ExecuteNonQuery(); //executre the cmd query
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        public void UnblockUser(String blockedName, String blockerName)
        {
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("UnblockUser", con); //name of your SQL procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@BlockedName", SqlDbType.VarChar, 15); //input of SQL stored procedure
                cmd.Parameters["@BlockedName"].Value = blockedName;

                cmd.Parameters.Add("@BlockerName", SqlDbType.VarChar, 15); //input of SQL stored procedure
                cmd.Parameters["@BlockerName"].Value = blockerName;

                cmd.ExecuteNonQuery(); //executre the cmd query
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        public int CheckIsBlocked(String blockedName, String blockerName)
        {
            int flag = 0;

            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("CheckIsBlocked", con); //name of your SQL procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@BlockedName", SqlDbType.VarChar, 15); //input of SQL stored procedure
                cmd.Parameters["@BlockedName"].Value = blockedName;

                cmd.Parameters.Add("@BlockerName", SqlDbType.VarChar, 15); //input of SQL stored procedure
                cmd.Parameters["@BlockerName"].Value = blockerName;

                SqlParameter output = new SqlParameter("@Flag", SqlDbType.Int);
                output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output);

                cmd.ExecuteNonQuery(); //executre the cmd query

                flag = Convert.ToInt32(output.Value);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }

            return flag;
        }

        public void GetBlockedUsers(String username, ref DataTable result)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("GetBlockedUsers", con); //name of your SQL procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 15); //input of SQL stored procedure
                cmd.Parameters["@UserName"].Value = username;

                cmd.ExecuteNonQuery();

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    da.Fill(ds); //get results

                result = ds.Tables[0]; //fill the results in ref input table 
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        public int SearchUsers(String userString, ref DataTable result)
        {
            int flag = 0;

            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("SearchUsers", con); //name of your SQL procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@UserString", SqlDbType.VarChar, 15); //input of SQL stored procedure
                cmd.Parameters["@UserString"].Value = userString;

                SqlParameter output = new SqlParameter("@Flag", SqlDbType.Int);
                output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output);

                cmd.ExecuteNonQuery();

                flag = Convert.ToInt32(output.Value);

                if (flag == 1)
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        da.Fill(ds); //get results

                    result = ds.Tables[0]; //fill the results in ref input table 
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }

            return flag;
        }

        public int SearchHashtags(String hashString, ref DataTable result)
        {
            int flag = 0;

            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("SearchHashtags", con); //name of your SQL procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@HashString", SqlDbType.VarChar, 40); //input of SQL stored procedure
                cmd.Parameters["@HashString"].Value = hashString;

                SqlParameter output = new SqlParameter("@Flag", SqlDbType.Int);
                output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output);

                cmd.ExecuteNonQuery();

                flag = Convert.ToInt32(output.Value);

                if (flag == 1)
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        da.Fill(ds); //get results

                    result = ds.Tables[0]; //fill the results in ref input table 
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }

            return flag;
        }

        public void GetConvos(String username, ref DataTable result)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd; 

            try
            {
                cmd = new SqlCommand("GetConvos", con); //name of your SQL procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 15); //input of SQL stored procedure
                cmd.Parameters["@UserName"].Value = username;

                cmd.ExecuteNonQuery();

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    da.Fill(ds); //get results

                result = ds.Tables[0]; //fill the results in ref input table
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        public void GetMessages(String username, String secUsername, ref DataTable result)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;
 
            try
            {
                cmd = new SqlCommand("GetMessages", con); //name of your SQL procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 15); //input of SQL stored procedure
                cmd.Parameters["@UserName"].Value = username;

                cmd.Parameters.Add("@SecUsername", SqlDbType.VarChar, 15); //input of SQL stored procedure
                cmd.Parameters["@SecUsername"].Value = secUsername;

                cmd.ExecuteNonQuery();

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    da.Fill(ds); //get results

                result = ds.Tables[0]; //fill the results in ref input table
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }

        }

        public void CreateMessage(String sender, String receiver, String body, DateTime time)
        {
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("CreateMessage", con); //name of your SQL procedure
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@SenderName", SqlDbType.VarChar, 15); //input of SQL stored procedure
                cmd.Parameters["@SenderName"].Value = sender;

                cmd.Parameters.Add("@ReceiverName", SqlDbType.VarChar, 15); //input of SQL stored procedure
                cmd.Parameters["@ReceiverName"].Value = receiver;

                cmd.Parameters.Add("@Body", SqlDbType.VarChar, 140); //input of SQL stored procedure
                cmd.Parameters["@Body"].Value = body;

                cmd.Parameters.Add("@MessageTime", SqlDbType.Time); //input of SQL stored procedure
                cmd.Parameters["@MessageTime"].Value = time.TimeOfDay;

                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }

        }

        public DataSet GetAllUsers()
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(connString);
            con.Open();

            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("Select UserName, FirstName, LastName from UserDetails", con);
                cmd.CommandType = CommandType.Text;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(ds);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }

            return ds;
        }

        public int DeleteUser(String UserName)
        {
            SqlConnection con = new SqlConnection(connString);
            con.Open();

            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("DeleteUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Username", SqlDbType.VarChar).Value = UserName;
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return -1;
            }
            finally
            {
                con.Close();
            }

            return 1;
        }

    }

}