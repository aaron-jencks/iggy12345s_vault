import time
import dataset_util_new
import dataset_util

if __name__ == "__main__":
	path = "./qa_Tools_and_Home_Improvement.json.gz"
	print("Testing the two configurations")
	print("<------------------------------------Process Implementation---------------------------------->")
	start = time.time()
	dataset_util.create_records(path)
	end = time.time()
	p_dur = end - start
	print("<-------------------------------------------------------------------------------------------->")
	print("Process implementation: {} secs".format(end - start))
	print("<------------------------------------Pool.map Implementation--------------------------------->")
	start = time.time()
	dataset_util_new.create_records(path)
	end = time.time()
	m_dur = end - start
	print("<-------------------------------------------------------------------------------------------->")
	print("Pool.map implementation: {} secs".format(end - start))
	print("<------------------------------------Results------------------------------------------------->")
	print("Process implementation: {} secs".format(p_dur))
	print("Pool.map implementation: {} secs".format(m_dur))
	print("Difference: {}".format(abs(m_dur - p_dur)))
