
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

//Speak function declarations

void Speak_Hourly(struct Employee* emp);
void Speak_Commission(struct Employee* emp);
void Speak_Senior(struct Employee* emp);

double GetPay_Hourly(struct Employee* emp);
double GetPay_Commission(struct Employee* emp);
double GetPay_Senior(struct Employee* s_emp);

void Construct_Hourly(struct HourlyEmployee* h_emp, int h_age, double h_rate, double h_hours);
void Construct_Commission(struct CommissionEmployee* c_emp, int c_age, double c_sales);
void Construct_Senior(struct SeniorSalesman* s_emp, int s_age, double s_sales);

//Vtable declarations

void* Vtable_Hourly[];
void* Vtable_Commission[];
void* Vtable_Senior[];

//Vtable initializations

void* Vtable_Hourly[] = { Speak_Hourly, GetPay_Hourly };
void* Vtable_Commission[] = { Speak_Commission, GetPay_Commission };
void* Vtable_Senior[] = { Speak_Senior, GetPay_Senior };
//void* Vtable_Senior[] = { Speak_Commission, GetPay_Senior };

double GetPay_Hourly(struct Employee* emp)
{
    struct HourlyEmployee* emp2;
    emp2 = (struct HourlyEmployee*)&emp;
    return emp2->hours * emp2->hourly_rate; // 90 * 9.50 = 855
}

double GetPay_Commission(struct Employee* emp)
{
    struct CommissionEmployee* emp2;
    emp2 = (struct CommissionEmployee*)&emp;
    return (emp2->sales_amount * .1) + 40000; // 80,000 *.1 + 40,000 = 48,000
}

void Speak_Hourly(struct Employee* emp) 
{
    struct Employee* emp2;
    emp2 = (struct HourlyEmployee*)&emp;
    printf("Employee made $%.2f ", GetPay_Hourly(&emp));
}

void Speak_Commission(struct Employee* emp)
{
    struct Employee* emp2;
    emp2 = (struct CommissionEmployee*)&emp;
    printf("Employee made $%.2f ", GetPay_Commission(&emp2));
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

void Speak_Senior(struct Employee* emp)
{
    struct Employee* emp2;
    emp2 = (struct SenorSalesman*)&emp;
    printf("Employee made $%.2f ", GetPay_Senior(&emp2));
}

double GetPay_Senior(struct Employee* emp)
{
    struct SeniorSalesman* emp2;
    emp2 = (struct SeniorSalesman*)&emp;
    if (emp2->age >= 40)
    {
        return (emp2->sales_amount * .2) + 50000 + (emp2->sales_amount * .05); // (100,000 *.2) + 50,000 + (100,000 * .05) = 75,000
    }
    else
    {
        return (emp2->sales_amount * .2) + 50000; // (100,000 *.2) + 50,000 = 70,000
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


int main()
{
    struct Employee* emp;
    printf("Choose a number: \n(1)hourly employee\n(2)commission employee\n(3)senior salesman\n");
    char input[25];
    int age;
    fgets(input, sizeof(input), stdin);
    if (strcmp(input, "hourly employee\n\0") == 0 || strcmp(input, "1\n\0") == 0) //if statements allow for number choice or lowercase of words
    {
        printf("Hourly Employee: \n");
        struct HourlyEmployee* hr = (struct HourlyEmployee*) malloc(sizeof(struct HourlyEmployee));
        printf("How old is employee? ");
        scanf_s("%d", &age);
        printf("What is the employee's pay rate? $");
        double pay;
        scanf_s("%lf", &pay);
        printf("What is the employee's hours? ");
        double hours;
        scanf_s("%lf", &hours);
        Construct_Hourly(&hr, age, pay, hours); //losing value after construct... hmm?
        emp = (struct Employee*)&hr;
        ((void (*)(struct Employee*))Vtable_Hourly[0])((struct Employee*)&emp);
    }
    else if (strcmp(input, "commission employee\0") == 0 || strcmp(input, "2\n\0") == 0)
    {
        printf("Commission Employee: \n");
        struct CommissionEmployee* cm = (struct CommissionEmployee*)malloc(sizeof(struct CommissionEmployee));
        printf("How old is commission employee? ");
        scanf_s("%d", &age);
        double sales;
        printf("What is the commission employee's sales? $");
        scanf_s("%lf", &sales);
        Construct_Commission(&cm, age, sales); //losing value after construct... hmm here too?
        emp = (struct Employee*)&cm;
        ((void (*)(struct Employee*))Vtable_Commission[0])((struct Employee*)&emp);
    }
    else if(strcmp(input, "senior employee\0") == 0 || strcmp(input, "3\n\0") == 0)
    {
        printf("Senior Salesman: \n");
        struct SeniorSalesman* snr = (struct SeniorSalesman*)malloc(sizeof(struct SeniorSalesman));
        printf("How old is senior employee? ");
        scanf_s("%d", &age);
        double sales;
        printf("What is the senior employee's sales? $"); //losing value after construct... hmm here too?
        scanf_s("%lf", &sales);
        Construct_Senior(&snr, age, sales);
        emp = (struct Employee*)&snr;
        ((void (*)(struct Employee*))Vtable_Senior[0])((struct Employee*)&emp);
    }
    else
    {
        printf("input error! BYE BYE");
    }
    exit(0);
}
