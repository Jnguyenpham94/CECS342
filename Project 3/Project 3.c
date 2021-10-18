
#include <stdlib.h>
#include <stdio.h>

struct Employee
{
    void **vtable;
    int age;
};

struct HourlyEmployee
{
    void **vtable;
    int age;
    double hourly_rate;
    double hours;
};

struct CommissionEmployee
{
    void **vtable;
    int age;
    double hourly_rate;
    double hours;
    double sales_amount;
};

//functions go HERE

void Speak_Hourly(struct Employee *emp) 
{
    emp = (struct HourlyEmployee*)emp;
    printf("I work for %d dollars per hour", emp->hours);
}

void GetPay_Hourly(struct Employee* emp)
{

}

void Construct_Hourly(struct HourlyEmployee* h_emp)
{

}

void Speak_Commission()
{

}

int main()
{
    printf("HELLO WORLD");
}
