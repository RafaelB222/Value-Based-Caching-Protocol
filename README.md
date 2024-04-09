# Value-Based-Caching-Protocol
A Client. Server, and Proxy Server built to implement value based caching when making image requests from the Client to the Server. 

To run the program, you can either run the BuildAndRun batch file or you can run the Caching_Solution.sln via Visual studio or the editor of your choice. 
 
A console window for each project will open along with the client GUI. 

Within the client GUI, you will then be able to retrieve a list of images available on the server by 
clicking the “Get Image List” button. 

To download an image, click on one of the image names displayed in the image names box. Then 
click the “Get Selected Image” button. The image will be downloaded, and an alert will be displayed showing the path of the downloaded 
image. 

The contents of the cache can be viewed by clicking the “View Cache Contents” button. 
This will open the cache GUI. Once the cache GUI has opened, you can view the cache’s log by 
clicking the “View Log” button. 

To view the hashes of the image fragments stored on the cache, click the “View Cache Items” button. 
To view a selected image fragment as a list of hexadecimal numbers, click on one of the image 
fragment hashes and then click the “View Selected Item” button. To clear the contents of the cache, click the “Clear Cache” button. 

To add an image to the images available to be downloaded from the server, click the “Open Server 
GUI” button on the client GUI. 

You can then click the “Add Image To Server” button and browse to the location of you image on your 
machine. The image will then be added to the server folder containing the images available for 
download. 

The image list can be refreshed by clicking the “Get Image List” on the client GUI again. 
