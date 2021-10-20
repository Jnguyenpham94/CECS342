
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
    struct HourlyEmployee* emp2 = (struct HourlyEmployee*) &emp;
    printf("I work for %f dollars per hour", emp2->hourly_rate);
}

void Speak_Commission(struct Employee* emp)
{
    struct CommissionEmployee* emp2 = (struct CommisionEmployee*)&emp;
    printf("I work for %f dollars per hour", emp2->hourly_rate);
}

double GetPay_Hourly(struct HourlyEmployee *h_emp)
{
    return h_emp->hours * h_emp->hourly_rate;
}

double GetPay_Commission(struct CommissionEmployee *c_emp)
{
    return (c_emp->sales_amount * .1) + 40000;
}

void* Vtable_Hourly[] = { Speak_Hourly, GetPay_Hourly };

void Construct_Hourly(struct HourlyEmployee *h_emp)
{
    h_emp->age = 0;
    h_emp->hourly_rate = 0;
    h_emp->hours = 0;
    h_emp->vtable = Vtable_Hourly;
}

void* Vtable_Commission[] = { Speak_Commission, GetPay_Commission };

void Construct_Commission(struct CommissionEmployee *c_emp) 
{
    c_emp->age = 0;
    c_emp->hourly_rate = 0;
    c_emp->hours = 0;
    c_emp->sales_amount = 0;
    c_emp->vtable = Vtable_Commission;
}

struct SeniorSalesman
{
    void** vtable;
    int age;
    double hourly_rate;
    double hours;
    double sales_amount;
};

double GetPay_Senior(struct SeniorSalesman *s_emp)
{
    if (s_emp->age >= 40)
    {
        return (s_emp->sales_amount * .2) + 50000 + (s_emp->sales_amount * .05);
    }
    else
    {
        return (s_emp->sales_amount * .2) + 50000;
    }
}

void* Vtable_Senior[] = { Speak_Commission, GetPay_Senior };

int main()
{
    struct Employee* emp;
    printf("Choose: hourly employee, commission employee, or senior salesman");
    char input[20];
    fgets(input, sizeof(input), stdin);
    if (input == "hourly employee") 
    {
        //HourlyEmployee* h = (HourlyEmployee*)malloc();
        printf("How old is employee?");
    }
    else if (input == "commission employee")
    {
        //CommissionEmployee* c = (CommissionEmployee*)malloc();
        printf("How old is employee?");
    }
    else if(input == "senior employee")
    {
        //SeniorSalesman* s = (SeniorSalesman*)malloc();
        printf("How old is employee?");
    }
    else
    {
        printf("input error");
    }
}
