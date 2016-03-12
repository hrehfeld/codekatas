import datetime

import random

def rand(x):
    return random.random() * x

begday = datetime.date(2016, 1, 1)
endday = datetime.date(2016, 1, 20)


def daterange(start_date, end_date):
    for n in range(int((end_date - start_date).days)):
        yield start_date + datetime.timedelta(n)


def workingday_range(beg, end):
    for d in daterange(beg, end):
        if d.weekday() != 6:
            yield d

days = [d for d in workingday_range(begday, endday)]

orders = [(days[i * 2], i + 1) for i in range(int(len(days) / 2))]



print(orders)

orderdays = list(set([o[0] for o in orders]))
summed_orders = [sum([o[1] for o in orders if o[0] == d]) for d in orderdays]
summed_orders = sorted(zip(orderdays, summed_orders), key=lambda p: p[0])

lastday = (endday - datetime.timedelta(days=1))
if summed_orders[-1][0] != lastday:
    summed_orders.append((lastday, 0))

print(summed_orders)


lastdate = begday - datetime.timedelta(days=1)
daysums = []
for date, s in summed_orders:
    n = len(list(workingday_range(lastdate, date)))
    assert(n > 0)
    print(n)
    for i in range(n):
        daysums.append(s / n)
    lastdate = date

print(len(daysums), daysums)
        
    

