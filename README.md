# EMarketBridge


EMarket bridge application was created to test the use of microservice and Masstransit structure with rabbitmq.

Within this microservice project, there are currently 3 microservices active at a basic level.
These are:
OrderProject,
StockProject,
SharedProject

To explain the purpose of these projects:

OrderProject

I have imagined for the moment that a person creates an order and the Order structure represents this order. When the product cart is created and the order is placed, the Order is first saved in the database and then some information about the Order is sent to rabbitmq with the help of OrderCreatedEvent and Masstransit in order to check the stock of the products in the Order.
It listens to StockOrderFailedEvent in order to listen to any error in stock transactions and to save the status of the related order in the database and notify the customer.

StockProject

It is a project where stock editing is done by checking whether the content of the order created by a person is appropriate. In addition to stock editing, new products can also be added to the stock via Swagger. This project listens to the OrderCreatedEvent sent to rabbitmq. When the relevant Event is triggered, the stock ordering operation is performed, and if an error occurs, a StockOrderFailedEvent is sent to rabbitmq to notify the OrderProject.

SharedProject
It is a project created for projects to communicate with each other. There are some constant values and events that can be used by other projects. Other projects can perform their operations by taking reference from this project.

------ Parts under development ---- 
ProductProject

It is planned to add, delete and edit products.

PaymentProject

It is planned as the place where the payment of the relevant order will be made after the stock transactions are successfully realized.
