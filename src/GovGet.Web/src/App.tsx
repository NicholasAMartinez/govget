import { useState } from 'react'
import { getPing } from './api/ping'
import type { PingResult } from './models/PingResult'

function App() {
  const [result, setResult] = useState<PingResult | null>(null)

  async function handlePing() {
    const pingResult = await getPing()
    setResult(pingResult)
  }

  return (
    <main>
      <button onClick={handlePing}>
        Ping
      </button>

      {result && (
        <div>
          <p>Status: {result.status}</p>
          <p>Timestamp: {result.timestamp}</p>
        </div>
      )}
    </main>
  )
}

export default App
