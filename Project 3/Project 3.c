
#include <stdlib.h>
#include <stdio.h>

struct Employee
{
    void** vtable;
    int age;
};

struct HourlyEmployee
{
    void** vtable;
    int age;
    double hourly_rate;
    double hours;
};

struct CommissionEmployee
{
    void** vtable;
    int age;
    double hourly_rate;
    double hours;
    double sales_amount;
};

struct SeniorSalesman
{
    void** vtable;
    int age;
    double hourly_rate;
    double hours;
    double sales_amount;
};

//Vtable declarations

void* Vtable_Hourly[2];
void* Vtable_Commission[2];
void* Vtable_Senior[2];

//Speak function declarations

void Speak_Hourly(struct Employee* emp);
void Speak_Commission(struct Employee* emp);

//((void (*)(struct Employee*))Vtable_Hourly[0])((struct Employee*)&emp)

double GetPay_Hourly(struct Employee* emp)
{
    return emp->hours * emp->hourly_rate;
}

double GetPay_Commission(struct Employee* emp)
{
    return (emp->sales_amount * .1) + 40000;
}

void Speak_Hourly(struct Employee* emp) 
{
    struct HourlyEmployee* emp2;
    emp2 = (struct HourlyEmployee*)&emp;
    printf("Employee made %.2f dollars", GetPay_Hourly(&emp2));
}

void Speak_Commission(struct Employee* emp)
{
    struct Employee* emp2;
    emp2 = (struct CommisionEmployee*)&emp;
    printf("Employee made %.2f dollars", GetPay_Commission(&emp2));
}

void Construct_Hourly(struct HourlyEmployee* h_emp, int h_age, double h_rate, double h_hours)
{
    h_emp->age = h_age;
    h_emp->hourly_rate = h_rate;
    h_emp->hours = h_hours;
    h_emp->vtable = Vtable_Hourly;
}

void Construct_Commission(struct CommissionEmployee* c_emp, int c_age, double c_sales) 
{
    c_emp->age = c_age;
    c_emp->hourly_rate = 0;
    c_emp->hours = 0;
    c_emp->sales_amount = c_sales;
    c_emp->vtable = Vtable_Commission;
}

double GetPay_Senior(struct SeniorSalesman* s_emp)
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

void Construct_Senior(struct SeniorSalesman* s_emp, int s_age, double s_sales)
{
    s_emp->age = s_age;
    s_emp->hourly_rate = 0;
    s_emp->hours = 0;
    s_emp->sales_amount = s_sales;
    s_emp->vtable = Vtable_Senior;
}

//Vtable initializations

void* Vtable_Hourly[] = { Speak_Hourly, GetPay_Hourly };
void* Vtable_Commission[] = { Speak_Commission, GetPay_Commission };
void* Vtable_Senior[] = { Speak_Commission, GetPay_Senior };

int main()
{
    struct Employee* emp;
    printf("Choose a number: \n(1)hourly employee\n(2)commission employee\n(3)senior salesman\n");
    char input[25];
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
        Construct_Hourly(&hr, age, pay, hours);
        emp = &hr;
        ((void (*)(struct Employee*))Vtable_Hourly[0])((struct Employee*)&emp);
    }
    else if (strcmp(input, "commission employee\0") == 0 || strcmp(input, "2\n\0") == 0)
    {
        struct CommissionEmployee* cm = (struct CommissionEmployee*)malloc(sizeof(struct CommissionEmployee));
        printf("How old is employee? ");
        scanf_s("%d", &age);
        double sales;
        printf("What is the employee's sales? ");
        scanf_s("%lf", &sales);
        Construct_Commission(&cm, age, sales);
        emp = &cm;
        ((void (*)(struct Employee*))Vtable_Commission[0])((struct Employee*)&emp);
    }
    else if(strcmp(input, "senior employee\0") == 0 || strcmp(input, "3\n\0") == 0)
    {
        struct SeniorSalesman* snr = (struct SeniorSalesman*)malloc(sizeof(struct SeniorSalesman));
        printf("How old is employee? ");
        scanf_s("%d", &age);
        double sales;
        printf("What is the employee's sales? ");
        scanf_s("%lf", &sales);
        Construct_Senior(&snr, age, sales);
        emp = &snr;
        ((void (*)(struct Employee*))Vtable_Commission[0])((struct Employee*)&emp);
    }
    else
    {
        printf("input error! BYE BYE");
    }
    exit(0);
}
