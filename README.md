# IndoorNavigation
Spec project

Abstract:

This project aims to develop a real-time mobile application for indoor navigation using Wi-Fi
trilateration for local positioning instead of GPS Triangulation, because of its inefficiency inside
the buildings. This framework will be supported by Augmented Reality to create an intuitive
navigation experience. The current existing applications for AR navigation make use of GPS,
which fails to give efficient results in indoor localization systems. To tackle this problem, we
devise an approach of estimation of local positioning by WiFi access points present inside the
building itself. While still inside the building, the user can directly teleport to other locations
virtually using our application.

Methodology and Working:

Wi-Fi trilateration estimates the distance between the receiver and three transmitters by
generating circles of radius around each access point as the distance from it, and their
intersection points will define the area where the receiver is.

Another method for localization is fingerprinting, in this technique there is matching between
previously calculated RSSI values with the real-time measured data. The mobile application that
the user downloads will be able to spot his location in some building, use it to accurately guide
him in locations found inside buildings by projecting a virtual character on top of the camera
such that the user can visualize both the real word and the character guiding him/her to the
desired location.

![trilateration](https://github.com/Swagat47/IndoorNavigation/blob/master/Progress/4.png)

Goals:
1. To map the ECE department building, enabling the new admissions to navigate through
the department without any external help.
2. AR will enhance the user experience, creating an intuitive user interface for users to learn
about the department in detail.
3. This expansion of this application for the complete campus of NIT Hamirpur can be used
during orientations to guide new students through the college.

Learning Outcomes:

Augmented Reality (AR) is a technology that overlays interactive digital elements — such as
text, images, video clips, sounds, 3D models and animations — into real-world environments.
Not only does AR enhance learning, but it also provides students with opportunities to create
their own content.
We learn about WiFi communication and networking protocols and familiarity with important
pathfinding algorithms.

Few ScreenShots :
![first](https://github.com/Swagat47/IndoorNavigation/blob/master/Progress/1.jpeg)
![SECOND](https://github.com/Swagat47/IndoorNavigation/blob/master/Progress/2.jpeg)
![third](https://github.com/Swagat47/IndoorNavigation/blob/master/Progress/3.jpeg)
