import requests
from bs4 import BeautifulSoup

# Загрузка HTML-страницы
url = "https://www.example.com"
response = requests.get(url)

# Создание объекта BeautifulSoup для парсинга HTML-кода
soup = BeautifulSoup(response.content, 'html.parser')

# Нахождение всех элементов <p> на странице и извлечение текста из них
paragraphs = soup.find_all('p')
for p in paragraphs:
    print(p.get_text())