import requests
from bs4 import BeautifulSoup

# �������� HTML-��������
url = "https://www.example.com"
response = requests.get(url)

# �������� ������� BeautifulSoup ��� �������� HTML-����
soup = BeautifulSoup(response.content, 'html.parser')

# ���������� ���� ��������� <p> �� �������� � ���������� ������ �� ���
paragraphs = soup.find_all('p')
for p in paragraphs:
    print(p.get_text())