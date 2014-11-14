 (C) Copyright 2014-2016 by AWS, Inc. 

 Permission to use, copy, modify, and distribute this software in
 object code form for any purpose and without fee is hereby granted, 
 provided that the above copyright notice appears in all copies and 
 that both that copyright notice and the limited warranty and
 restricted rights notice below appear in all supporting 
 documentation.

 AWS PROVIDES THIS PROGRAM "AS IS" AND WITH ALL FAULTS. 
 AWS SPECIFICALLY DISCLAIMS ANY IMPLIED WARRANTY OF
 MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE.  AWS, INC. 
 DOES NOT WARRANT THAT THE OPERATION OF THE PROGRAM WILL BE
 UNINTERRUPTED OR ERROR FREE.

 Use, duplication, or disclosure by the U.S. Government is subject to 
 restrictions set forth in FAR 52.227-19 (Commercial Computer
 Software - Restricted Rights) and DFAR 252.227-7013(c)(1)(ii)
 (Rights in Technical Data and Computer Software), as applicable.


1. 本程序集适用于AUTOCAD 2010-2012各版本。用于输入、提取轴测图中的数据并输出至EXCEL表格便于统计。
2. 使用时请在D：盘下建立文件夹“D:\ISO\”。将EXCEL模板拷贝到该目录下。
3. 将pipestate.dll拷贝至任意位置。
4. 启动AUTOCAD程序或打开任一文件。
5. 在AUTOCAD命令行输入" netload"命令，并导航至pipestate.dll所在位置，加载程序集。
6. 在命令行输入awsiso之后可以使用右键菜单进行下面的操作。也可以按下面的方法直接输入命令进行操作。
7. 输入数据：在命令行输入"input"命令，之后按界面要求输入数据。
8. 将数据导出至EXCEL文件：在命令行输入"output"命令。
9. 将管道数据显示在图形中：在命令行输入"display"命令。
10. 如需在图形中插入材料清单，可以在EXCEL表中选中所需数据，Ctrl+C复制数据。在AUTOCAD中选择特殊性粘贴，AUTOCAD图元即可将表格插入图形文件。

如有程序有任何问题请联系赵皓翰(E-mail:zhao.henry@aws-tec.com)。