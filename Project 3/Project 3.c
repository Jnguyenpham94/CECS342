
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

//Speak functions begin

void Speak_Hourly(struct Employee *emp) 
{
    struct HourlyEmployee* emp2 = (struct HourlyEmployee*) &emp;
    printf("I made %f dollars", emp2->hourly_rate);
}

void Speak_Commission(struct Employee* emp)
{
    struct CommissionEmployee* emp2 = (struct CommisionEmployee*) &emp;
    printf("I work for %f dollars per hour", emp2->hourly_rate);
}

//GetPay functions begin

double GetPay_Hourly(struct HourlyEmployee *emp)
{
    return emp->hours * emp->hourly_rate;
}

double GetPay_Commission(struct CommissionEmployee *emp)
{
    return (emp->sales_amount * .1) + 40000;
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

//Senior Salesman "class" begins here

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

void Construct_Senior(struct SeniorSalesman* s_emp)
{
    s_emp->age = 0;
    s_emp->hourly_rate = 0;
    s_emp->hours = 0;
    s_emp->sales_amount = 0;
    s_emp->vtable = Vtable_Commission;
}

int main()
{
    struct Employee* emp;
    printf("Choose: \n(1)hourly employee\n(2)commission employee\n(3)senior salesman\n");
    char input[20];
    int age;
    fgets(input, sizeof(input), stdin);
    if (strcmp(input, "hourly employee\n\0") == 0 || strcmp(input, "1\n\0") == 0)
    {
        struct HourlyEmployee* hr = (struct HourlyEmployee*) malloc(sizeof(struct HourlyEmployee));
        printf("How old is employee? ");
        scanf_s("%d", &age);
        printf("What is the employee's pay rate? ");
        double pay;
        scanf_s("%lf", &pay);
        printf("What is the employee's hours? ");
        double hours;
        scanf_s("%lf", &hours);
        Construct_Hourly(&hr);
        emp = &hr;
        ((void (*)(struct Employee*))Vtable_Hourly[0])((struct Employee*)&hr);
    }
    else if (strcmp(input, "commission employee\0") == 0 || strcmp(input, "2\n\0") == 0)
    {
        struct CommissionEmployee* cm = (struct CommissionEmployee*)malloc(sizeof(struct CommissionEmployee));
        printf("How old is employee? ");
        scanf_s("%d", &age);
        double sales;
        printf("What is the employee's sales? ");
        scanf_s("%lf", &sales);
        Construct_Commission(&cm);
        emp = &cm;
        ((void (*)(struct Employee*))Vtable_Commission[0])((struct Employee*)&cm);
    }
    else if(strcmp(input, "senior employee\0") == 0 || strcmp(input, "3\n\0") == 0)
    {
        struct SeniorSalesman* snr = (struct SeniorSalesman*)malloc(sizeof(struct SeniorSalesman));
        printf("How old is employee? ");
        scanf_s("%d", &age);
        double sales;
        printf("What is the employee's sales? ");
        scanf_s("%lf", &sales);
        Construct_Senior(&snr);
        emp = &snr;
        ((void (*)(struct Employee*))Vtable_Commission[0])((struct Employee*)&snr);
    }
    else
    {
        printf("input error! BYE BYE");
    }
}
