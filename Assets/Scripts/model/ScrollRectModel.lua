---------------------------------------------------------------------
-- U3DAvatar (C) CompanyName, All Rights Reserved
-- Created by: AuthorName
-- Date: 2022-07-28 14:50:19
---------------------------------------------------------------------

-- To edit this template in: Data/Config/Template.lua
-- To disable this template, check off menuitem: Options-Enable Template File

---@class ScrollRectModel
ScrollRectModel = {}
ScrollRectModel.datasAndIndex={}
--ScrollRectModel.needDispose={}
ScrollRectModel.datas={}

function ScrollRectModel.removeUnUseItem(startNum,endNum)
	local dispose={}
	--print("dataAndIndex length")
	--print(#ScrollRectModel.datasAndIndex)
	for i, v in pairs(ScrollRectModel.datasAndIndex) do
		if ScrollRectModel.datasAndIndex[i]<startNum or ScrollRectModel.datasAndIndex[i]>=endNum then
			--dispose[#dispose]=i
			table.insert(dispose,i)
		end
	end
	--print("startNum :")
	--print(startNum)
	--print(#dispose)
	print("before remove ")
	for k, v in pairs(ScrollRectModel.datasAndIndex) do
		print(v)
	end
	for i, v in pairs(dispose) do
		ScrollRectModel.datasAndIndex[v]=nil
		ScrollRectModel.ScrollRectControl.RemoveItem(v)
	end
	print("after")
	for k, v in pairs(ScrollRectModel.datasAndIndex) do
		print(v)
	end
	--print("remove over")
end

function ScrollRectModel.generaNewItem(startNum,endNum)
	for i = startNum,endNum-1,1 do
		if i<=#ScrollRectModel.datas then
			if(isContanisValue(ScrollRectModel.datasAndIndex,i)==false) then
				--print("do not have")
				--print(i)
				ScrollRectModel.addNewItem(i)
			end
		end
	end
end

function ScrollRectModel.addToDatasAndIndex(index,go)
	--print("index is :")
	--print(index)
	--print("go is :")
	--print(go)
	print("addNew")
	print(index)
	--print(go==nil)
	ScrollRectModel.datasAndIndex[go]=index
	--print("length is ")
	--print(ScrollRectModel.getLenOfTable(ScrollRectModel.datasAndIndex))
end
function  ScrollRectModel.setDatas(datas)
	ScrollRectModel.datas=datas
end

function ScrollRectModel.getDatasLength()
	return  #ScrollRectModel.datas
end

function ScrollRectModel.addNewItem(index)
	--print(index)
		ScrollRectModel.ScrollRectControl.SetNewItem(index,ScrollRectModel.datas[index])
end

function  deleteItem(go)
	ScrollRectModel.ScrollRectControl.RemoveItem(go)
end

function ScrollRectModel.clearRecord()
	    ScrollRectModel.datasAndIndex={}
end

function ScrollRectModel.setScrollControl(scrollControl)
	ScrollRectModel.ScrollRectControl=scrollControl
end

function isContanisValue(data,value)
	for k, v in pairs(data) do
		if v==value then
			return  true
		end
	end
	return  false
end
function ScrollRectModel.initItemRecord(index)
	ScrollRectModel.ScrollRectControl.GnenerNewItem(index,ScrollRectModel.datas[index])
end

function  ScrollRectModel.getLenOfTable(tab)
	local num=0
	for k, v in pairs(tab) do
		num=num+1
	end
	return  num
end

return  ScrollRectModel