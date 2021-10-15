
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
    double sales_amount;
};

//functions go HERE

int main()
{
    printf("HELLO WORLD");
}
