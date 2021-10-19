
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

//global vtable arrays

void* Vtable_Hourly[];
void* Vtable_Commission[];

//functions go HERE

void Speak_Hourly(struct Employee *emp) 
{
    struct HourlyEmployee* emp2 = (struct HourlyEmployee*) &emp;
    printf("I work for %f dollars per hour", emp2->hourly_rate);
}

void GetPay_Hourly(struct Employee *emp)
{

}

void Construct_Hourly(struct HourlyEmployee *h_emp)
{
    h_emp->age = 0;
    h_emp->hourly_rate = 0;
    h_emp->hours = 0;
    h_emp->vtable = Vtable_Hourly;
}

void Speak_Commission(struct Employee *emp)
{
    struct CommissionEmployee* emp2 = (struct CommisionEmployee*)&emp;
    printf("I work for %f dollars per hour", emp2->hourly_rate);
}

void GetPay_Commission(struct Employee *emp)
{

}

void Construct_Commission(struct CommissionEmployee *c_emp) 
{

}


int main()
{
    struct Employee emp;
    Speak_Hourly(&emp);
}
