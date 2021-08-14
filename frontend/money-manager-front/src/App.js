import { useState, useEffect } from "react"
import Header from './components/Header'
import Tasks from './components/Tasks';
import AddTask from './components/AddTask';
import Movements from "./components/Movements";
import AddCategory from "./components/AddCategory";

const App = () => {
  const [showFormAddTask, setShowFormAddTask] = useState(false)
  const [tasks, setTasks] = useState([])
  const [movements, setMovements] = useState([])

  const [showAdmin,setShowAdmin] = useState(false)
 const [categories, setCategories] = useState([])
  useEffect(() => {
    const getTasks = async () => {
      const tasksFromServer = await fetchTasks()
      setTasks(tasksFromServer)
    }

    const getMovements = async () => {
      const movementsFromServer = await fetchMovements()
      setMovements(movementsFromServer)
    }

   // getTasks()

    getMovements()
  }, [])

  // FetchTasks

  const fetchTasks = async () => {
    const res = await fetch('http://localhost:5000/tasks')
    const data = await res.json()

    return data
  }

  const fetchTask = async (id) => {
    const res = await fetch(`http://localhost:5000/tasks/${id}`)
    const data = await res.json()

    return data
  }

  const fetchMovements = async () => {
    const res = await fetch('https://localhost:5001/check/api/v1/movements')
    const data = await res.json()

    console.log(data)
    return data
  }

  // Delete task
  const deleteTask = async (id) => {
    await fetch(`http://localhost:5000/tasks/${id}`, { method:'DELETE',})  
    setTasks(tasks.filter((task) => task.id !== id))
  }

  // toggle reminder

  const toggleReminder = async (id) => {
    const taskToToggle = await fetchTask(id)
    const updTask = {...taskToToggle, reminder: !taskToToggle.reminder}

    const res = await fetch (`http://localhost:5000/tasks/${id}`, 
    {
      method: 'PUT',
      headers: {
        'Content-type': 'application/json' },
        body:JSON.stringify(updTask)
      })

      const data = await res.json()

    setTasks(
      tasks.map(
        (task) => task.id === id ? { ...task, reminder: data.reminder } : task
      )
    )
  }

  const addTask = async (task) => {
    const res = await fetch ('http://localhost:5000/tasks', 
    {
      method: 'POST',
      headers: {
        'Content-type': 'application/json' },
        body:JSON.stringify(task)
      })


      const data = await res.json()
  setTasks([...tasks, data])

    // const id = Math.floor(Math.random() * 10000) + 1
    // const newTask = { id, ...task }
    // setTasks([...tasks, newTask])

  }


  const addCategory =  (category) => {
    const id = Math.floor(Math.random() * 10000) + 1
    const newCategory = { id, ...category }
    setCategories([...categories, newCategory])
    console.log('Add category',category)
    console.log('Add category',newCategory)
    console.log('Add category',categories)
  }

  return (
    // <div className="container">
    <div>
      <Header title="Task Trackers" onAdd={() => setShowFormAddTask(!showFormAddTask)} showAdd={showFormAddTask} />
      {/* {showFormAddTask && <AddTask onAdd={addTask} />}
      {
        tasks.length > 0 ?
          (<Tasks tasks={tasks} onDelete={deleteTask} onToggle={toggleReminder} />)
          : ('No Tasks to Show')
      } */}


      {/* <Movements movements={movements}/> */}
      <AddCategory categories={categories} onAdd={addCategory}/>
    </div>
  )
}

export default App;

