import { useState } from 'react'
import { getPing } from './api/ping'
import type { PingResult } from './models/PingResult'
import { getCount } from './api/count'
import type { CountResult } from './models/CountResult'

function App() {
  const [result, setResult] = useState<PingResult | null>(null)
  const [count, setCount] = useState<CountResult | null>(null)

  async function handlePing() {
    const pingResult = await getPing()
    setResult(pingResult)
  }

  async function handleCount() {
    const countResult = await getCount()
    setCount(countResult)
  }

  return (
    <main style={{display: 'flex', flexDirection: 'column', gap: '1rem', alignItems: 'center', justifyContent: 'center', height: '100vh'}}>
      
      <button 
        style={{padding: '1rem 2rem', width: '10rem', fontSize: '1.25rem', borderRadius: '0.5rem', backgroundColor: '#007bff', color: '#fff', border: 'none', cursor: 'pointer'}}
        onClick={handlePing}
      >
        Ping
      </button>

      <button 
        style={{padding: '1rem 2rem', width: '10rem', fontSize: '1.25rem', borderRadius: '0.5rem', backgroundColor: '#007bff', color: '#fff', border: 'none', cursor: 'pointer'}}
        onClick={handleCount}
      >
        Count
      </button>

      {result && (
        <div>
          <p>Status: {result.status}</p>
          <p>Timestamp: {result.timestamp}</p>
        </div>
      )}
      {count && (
        <div>
          <p>Number of earthquakes in the last 30 days: {count.count}</p>
        </div>
      )}
    </main>
  )
}

export default App
