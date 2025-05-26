data = [
  {
    "FirstName": "Emily",
    "LastName": "Johnson",
    "BirthDate": "1990-05-15",
    "StartAt": "2018-09-01",
    "EndAt": "2022-06-30",
    "Degree": "Computer Science"
  },
  {
    "FirstName": "Michael",
    "LastName": "Williams",
    "BirthDate": "1988-11-22",
    "StartAt": "2017-08-15",
    "EndAt": "2021-05-20",
    "Degree": "Business Administration"
  },
  {
    "FirstName": "Sarah",
    "LastName": "Brown",
    "BirthDate": "1992-03-08",
    "StartAt": "2019-01-10",
    "EndAt": "2023-04-25",
    "Degree": "Psychology"
  },
  {
    "FirstName": "David",
    "LastName": "Jones",
    "BirthDate": "1991-07-30",
    "StartAt": "2016-09-05",
    "EndAt": "2020-06-15",
    "Degree": "Electrical Engineering"
  },
  {
    "FirstName": "Jennifer",
    "LastName": "Garcia",
    "BirthDate": "1989-09-12",
    "StartAt": "2018-08-20",
    "EndAt": "2022-05-18",
    "Degree": "Biology"
  },
  {
    "FirstName": "Robert",
    "LastName": "Miller",
    "BirthDate": "1993-02-28",
    "StartAt": "2020-01-15",
    "EndAt": "2024-06-10",
    "Degree": "Mathematics"
  },
  {
    "FirstName": "Jessica",
    "LastName": "Davis",
    "BirthDate": "1990-12-05",
    "StartAt": "2017-09-01",
    "EndAt": "2021-06-20",
    "Degree": "English Literature"
  },
  {
    "FirstName": "Christopher",
    "LastName": "Rodriguez",
    "BirthDate": "1987-06-18",
    "StartAt": "2015-08-25",
    "EndAt": "2019-05-30",
    "Degree": "Mechanical Engineering"
  },
  {
    "FirstName": "Amanda",
    "LastName": "Martinez",
    "BirthDate": "1994-04-22",
    "StartAt": "2021-09-10",
    "EndAt": "2025-06-15",
    "Degree": "Nursing"
  },
  {
    "FirstName": "Daniel",
    "LastName": "Hernandez",
    "BirthDate": "1992-08-14",
    "StartAt": "2019-08-15",
    "EndAt": "2023-05-20",
    "Degree": "Chemistry"
  },
  {
    "FirstName": "Elizabeth",
    "LastName": "Lopez",
    "BirthDate": "1991-01-30",
    "StartAt": "2018-01-10",
    "EndAt": "2022-04-25",
    "Degree": "History"
  },
  {
    "FirstName": "Matthew",
    "LastName": "Gonzalez",
    "BirthDate": "1989-10-17",
    "StartAt": "2017-09-05",
    "EndAt": "2021-06-15",
    "Degree": "Physics"
  },
  {
    "FirstName": "Ashley",
    "LastName": "Wilson",
    "BirthDate": "1993-07-03",
    "StartAt": "2020-08-20",
    "EndAt": "2024-05-18",
    "Degree": "Sociology"
  },
  {
    "FirstName": "Andrew",
    "LastName": "Anderson",
    "BirthDate": "1990-11-25",
    "StartAt": "2018-01-15",
    "EndAt": "2022-06-10",
    "Degree": "Political Science"
  },
  {
    "FirstName": "Nicole",
    "LastName": "Thomas",
    "BirthDate": "1992-05-19",
    "StartAt": "2019-09-01",
    "EndAt": "2023-06-20",
    "Degree": "Art History"
  },
  {
    "FirstName": "Joshua",
    "LastName": "Taylor",
    "BirthDate": "1988-12-08",
    "StartAt": "2016-08-25",
    "EndAt": "2020-05-30",
    "Degree": "Economics"
  },
  {
    "FirstName": "Megan",
    "LastName": "Moore",
    "BirthDate": "1994-02-14",
    "StartAt": "2021-09-10",
    "EndAt": "2025-06-15",
    "Degree": "Communications"
  },
  {
    "FirstName": "Kevin",
    "LastName": "Jackson",
    "BirthDate": "1991-09-27",
    "StartAt": "2019-08-15",
    "EndAt": "2023-05-20",
    "Degree": "Environmental Science"
  },
  {
    "FirstName": "Stephanie",
    "LastName": "Martin",
    "BirthDate": "1993-03-11",
    "StartAt": "2020-01-10",
    "EndAt": "2024-04-25",
    "Degree": "Philosophy"
  },
  {
    "FirstName": "Ryan",
    "LastName": "Lee",
    "BirthDate": "1989-06-24",
    "StartAt": "2017-09-05",
    "EndAt": "2021-06-15",
    "Degree": "Architecture"
  }
]
import urllib3
from urllib.parse import urlencode
from urllib.parse import quote
import requests as req

urllib3.disable_warnings(urllib3.exceptions.InsecureRequestWarning)
def json_to_url(data):
    url_encoded_string = urlencode(data)
    return url_encoded_string

logindata = {'UserName' : 'HamedSHF', 'Password': 'secret@123', 'RememberMe': 'true'}
loginurl = 'https://localhost:7244/Admin/GetTokenTest'
response = req.post(loginurl, data=json_to_url(logindata), verify=False, headers={'Content-Type': 'application/x-www-form-urlencoded'})


posturl = 'https://localhost:7145/Admin/RegisterTeacher'
token = response.content
token = token.decode(encoding="utf-8") #decode bytes to utf-8 string
token = quote(f"Bearer {token}") #convert token to url encoded string 
for item in data:
    request = req.post(posturl,data=json_to_url(item), verify=False, headers={'Content-Type': 'application/x-www-form-urlencoded'},
                       cookies={'Authorization':f"{token}"})