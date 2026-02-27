# Programming Principles in ATMSystem Project

## Область застосування

Аналіз принципів програмування виконано тільки для проєкту, що знаходиться у папці **lab02/ATMSystem**.

## Introduction

Цей проєкт реалізує систему банкомату (ATM). 
Основна мета — продемонструвати застосування принципів чистого програмування та ООП.

---

## 1. Single Responsibility Principle (SRP)

Кожен клас у проєкті відповідає лише за одну логічну відповідальність:

- Account.cs — представляє банківський акаунт та містить логіку роботи з балансом.
- Bank.cs — керує колекцією акаунтів.
- AutomatedTellerMachine.cs — відповідає за логіку банкомату.
- Program.cs — точка входу в програму.

Приклади:

Account:
[Account.cs](./lab02/ATMSystem/Account.cs)

Bank:
[Bank.cs](./lab02/ATMSystem/Bank.cs)

ATM:
[AutomatedTellerMachine.cs](./lab02/ATMSystem/AutomatedTellerMachine.cs)

Це демонструє розділення відповідальностей та чистішу архітектуру.

---

## 2. Encapsulation (Інкапсуляція)

Поля класів приховані через private, а доступ до них здійснюється через методи.

Наприклад, у Account.cs баланс не змінюється напряму, а через методи:

[Account.cs](./lab02/ATMSystem/Account.cs#L10-L30)

Це запобігає неконтрольованій зміні стану об'єкта.

---

## 3. Separation of Concerns

Логіка програми розділена на окремі частини:

- Взаємодія з користувачем — Program.cs
- Бізнес-логіка — AutomatedTellerMachine.cs
- Зберігання акаунтів — Bank.cs

Program.cs:
[Program.cs](./lab02/ATMSystem/Program.cs)

Такий підхід робить код більш зрозумілим і підтримуваним.

---

## 4. KISS (Keep It Simple, Stupid)

Методи реалізовані без зайвої складності. 
Операції зняття та поповнення балансу виконуються через прості умови.

Наприклад:
[Account.cs](./lab02/ATMSystem/Account.cs#L25-L51)

Логіка є прямолінійною та зрозумілою.

---

## 5. Basic OOP Principles

Проєкт використовує об’єктно-орієнтований підхід:

- Класи
- Методи
- Конструктори
- Інкапсуляція

Наприклад створення об’єктів у Program.cs:
[Program.cs](./lab02/ATMSystem/Program.cs#L23-L28)

Це відповідає принципам об’єктно-орієнтованого програмування.

---

## 6. Input Validation

Я перевіряю введені користувачем дані перед їх використанням.

[Program.cs](./lab02/ATMSystem/Program.cs)

Використовується метод TryParse для захисту від некоректного вводу.
