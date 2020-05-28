# Description
Employee payroll for an arbitrary point in time.

There is a company, the company may have employees. An employee is characterized by a name, date of entry to work, and base rate (for simplicity, this default value is the same for all types of employees).

There are 3 types of employees - Employee, Manager, Sales. Each employee can have a boss. Each employee except Employee can have subordinates.

Employee employee salary is the base rate plus 3% for each year of work in the company, but not more than 30% of the total allowance.

Manager employee salary is the base rate plus 5% for each year of work in the company (but not more than 40% of the total allowance) and 0.5% of the salary of all first-level subordinates.

Sales employee salary is the base rate plus 1% for each year of work in the company (but not more than 35% of the total allowance for work experience) and 0.3% of the salary of all subordinates of all levels

Employees (except Employee) can have any number of subordinates of any kind.

# ToDo
- Implement database layer.
- Refactor the BountyCalculationRules class in which the parameters for calculation are stored. Singleton must be replaced with a factory + strategy in order to get away from the switch case.
- Calculate all method in UI.
- Delete and Update method for Person entity.
- Fix Calculate dialog in UI.
- Write "Required" section
