#!/usr/bin/env python
# -*- coding: utf-8 -*-
import cgi

html_body = """
<html>
<head><meta char charset="utf-8"></head>
<body>
<p>Yourname is  %s </p>
</body></html>
"""

form = cgi.FieldStorage()


print("Content-type: text/html")
print(html_body % form['name'].value)
